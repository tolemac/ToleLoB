using System;
using ToleLoB.DependencyResolver;

namespace ToleLoB.CQRS.Queries
{
    public class QuerySystem
    {
        private IDependencyResolver _resolver;
        private QueryResultCache _cache;
        protected TimeSpan? MaxCacheDuration { get; set; }
        public QuerySystem(IDependencyResolver resolver)
        {
            _resolver = resolver;
            _cache = new QueryResultCache();
        }

        public QuerySystem(IDependencyResolver resolver, TimeSpan? maxCacheDuration = null)
            : this(resolver)
        {
            MaxCacheDuration = maxCacheDuration;
        }

        public void PurgeQueriesByStateItems(object[] stateItems)
        {
            _cache.PurgeByStateItems(stateItems);
        }

        public bool AcceptCachedResult(ICachedQueryResult result)
        {
            if (MaxCacheDuration.HasValue)
            {
                return MaxCacheDuration.Value > DateTime.Now - result.When;
            }
            return true;
        }

        public TOut Run<TQuery, TIn, TOut>(TIn parameters)
            where TQuery : IQuery<TIn, TOut>
        {
            var query = _resolver.Resolve<TQuery>();

            var cachedResult = _cache.Get(query, parameters);
            if (cachedResult != null)
            {
                if (query.AcceptCachedResult(cachedResult) && AcceptCachedResult(cachedResult))
                {
                    return (TOut)cachedResult.Result;
                }
                cachedResult.Purge();
            }

            var result = query.Run(parameters);
            _cache.Add(query, parameters, result);

            return result;
        }
    }
}