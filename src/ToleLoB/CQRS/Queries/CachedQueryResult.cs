using System;

namespace ToleLoB.CQRS.Queries
{
    public interface ICachedQueryResult
    {
        string ParameterHash { get; }
        Type QueryType { get; }
        DateTime When { get; }
        object Result { get; }
        void Purge();
    }

    internal class CachedQueryResult : ICachedQueryResult
    {
        private QueryResultCache _cache;
        public Type QueryType { get; private set; }
        public IQuery Query { get; private set; }
        public string ParameterHash { get; private set; }
        public object Result { get; private set; }
        public DateTime When { get; private set; }
        public CachedQueryResult(QueryResultCache cache, IQuery query, string parameterHash, object result)
        {
            _cache = cache;
            Query = query;
            QueryType = query.GetType();
            ParameterHash = parameterHash;
            Result = result;
            When = DateTime.Now;
        }

        public void Purge()
        {
            _cache.PurgeResult(this);
        }
    }
}