using System;
using System.Linq.Expressions;

namespace ToleLoB.Sql.Where
{
    public class SqlWhere : SqlWhereBase
    {
        public SqlWhere Set<TEntity1>(
            Expression<Func<TEntity1, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere Set<TEntity1, TEntity2>(
            Expression<Func<TEntity1, TEntity2, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere Set<TEntity1, TEntity2, TEntity3>(
            Expression<Func<TEntity1, TEntity2, TEntity3, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere Set<TEntity1, TEntity2, TEntity3, TEntity4>(
            Expression<Func<TEntity1, TEntity2, TEntity3, TEntity4, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere And<TEntity1>(
            Expression<Func<TEntity1, bool>> expression)
        {
            base.And((Expression)expression);
            return this;
        }
        public SqlWhere And<TEntity1, TEntity2>(
            Expression<Func<TEntity1, TEntity2, bool>> expression)
        {
            base.And((Expression)expression);
            return this;
        }
        public SqlWhere And<TEntity1, TEntity2, TEntity3>(
            Expression<Func<TEntity1, TEntity2, TEntity3, bool>> expression)
        {
            base.And((Expression)expression);
            return this;
        }
        public SqlWhere And<TEntity1, TEntity2, TEntity3, TEntity4>(
            Expression<Func<TEntity1, TEntity2, TEntity3, TEntity4, bool>> expression)
        {
            base.And((Expression)expression);
            return this;
        }
    }


}