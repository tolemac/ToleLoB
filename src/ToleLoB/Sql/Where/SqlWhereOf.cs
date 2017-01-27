using System;
using System.Linq.Expressions;

namespace ToleLoB.Sql.Where
{
    public class SqlWhere<TMainTable> : SqlWhereBase
    {
        public SqlWhere<TMainTable> Set(Expression<Func<TMainTable, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere<TMainTable> Set<TEntity1>(
            Expression<Func<TMainTable, TEntity1, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere<TMainTable> Set<TEntity1, TEntity2>(
            Expression<Func<TMainTable, TEntity1, TEntity2, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere<TMainTable> Set<TEntity1, TEntity2, TEntity3>(
            Expression<Func<TMainTable, TEntity1, TEntity2, TEntity3, bool>> expression)
        {
            base.Set(expression);
            return this;
        }
        public SqlWhere<TMainTable> Set<TEntity1, TEntity2, TEntity3, TEntity4>(
            Expression<Func<TMainTable, TEntity1, TEntity2, TEntity3, TEntity4, bool>> expression)
        {
            base.Set(expression);
            return this;
        }

        public SqlWhere<TMainTable> And(Expression<Func<TMainTable, bool>> expression)
        {
            base.And(expression);
            return this;
        }
        public SqlWhere<TMainTable> And<TEntity1>(
            Expression<Func<TMainTable, TEntity1, bool>> expression)
        {
            base.And(expression);
            return this;
        }
        public SqlWhere<TMainTable> And<TEntity1, TEntity2>(
            Expression<Func<TMainTable, TEntity1, TEntity2, bool>> expression)
        {
            base.And(expression);
            return this;
        }
        public SqlWhere<TMainTable> And<TEntity1, TEntity2, TEntity3>(
            Expression<Func<TMainTable, TEntity1, TEntity2, TEntity3, bool>> expression)
        {
            base.And(expression);
            return this;
        }
        public SqlWhere<TMainTable> And<TEntity1, TEntity2, TEntity3, TEntity4>(
            Expression<Func<TMainTable, TEntity1, TEntity2, TEntity3, TEntity4, bool>> expression)
        {
            base.And(expression);
            return this;
        }
    }
}