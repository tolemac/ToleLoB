using System.Collections.Generic;
using System;

namespace ToleLoB.CQRS.Queries
{
    public interface IQuery
    {
        object Run(object parameters);
        object[] QueryedStateItems { get; }
        bool AcceptCachedResult(ICachedQueryResult result);
    }
    public abstract class Query : IQuery
    {
        protected TimeSpan? MaxCacheDuration { get; set; }
        protected List<object> QueryedStateItemsList = new List<object>();
        public object[] QueryedStateItems { get { return QueryedStateItemsList.ToArray(); } }
        public abstract object Run(object parameters);
        public bool AcceptCachedResult(ICachedQueryResult result)
        {
            if (MaxCacheDuration.HasValue)
            {
                return MaxCacheDuration.Value > DateTime.Now - result.When;
            }
            return true;
        }
    }

    public interface IQuery<TIn, TOut> : IQuery
    {
        TOut Run(TIn parameters);
    }

    public abstract class Query<TIn, TOut> : Query, IQuery<TIn, TOut>
    {
        public override object Run(object parameters)
        {
            return Run((TIn)parameters);
        }
        public abstract TOut Run(TIn parameters);
    }
}