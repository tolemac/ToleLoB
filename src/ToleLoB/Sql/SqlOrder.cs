using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToleLoB.Sql
{
    internal enum OrderDirection { Ascend, Descend };
    public class SqlOrder
    {
        internal IList<Tuple<OrderDirection, Expression>> ExpressionList { get; private set; }
        public SqlOrder()
        {
            ExpressionList = new List<Tuple<OrderDirection, Expression>>();
        }
        private SqlOrder Reset()
        {
            ExpressionList.Clear();
            return this;
        }
        private SqlOrder Ascend(Expression expression)
        {
            ExpressionList.Add(new Tuple<OrderDirection, Expression>(OrderDirection.Ascend, expression));
            return this;
        }
        private SqlOrder Descend(Expression expression)
        {
            ExpressionList.Add(new Tuple<OrderDirection, Expression>(OrderDirection.Descend, expression));
            return this;
        }

        public SqlOrder Ascend<TEntity>(Expression<Func<TEntity, object>> expression)
        {
            return Ascend((Expression)expression);
        }

        public SqlOrder Descend<TEntity>(Expression<Func<TEntity, Object>> expression)
        {
            return Descend((Expression)expression);
        }

    }
}