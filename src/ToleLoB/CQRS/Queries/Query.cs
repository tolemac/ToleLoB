namespace ToleLoB.CQRS.Queries
{
    public interface IQuery
    {
        object Run(object parameters);
    }
    public abstract class Query : IQuery
    {
        public abstract object Run(object parameters);
    }

    public interface IQuery<TIn, TOut> : IQuery
    {
        TOut Run(TIn parameters);
    }

    public abstract class Query<TIn, TOut> : IQuery<TIn, TOut>
    {
        public object Run(object parameters)
        {
            return Run((TIn)parameters);
        }
        public abstract TOut Run(TIn parameters);
    }
}