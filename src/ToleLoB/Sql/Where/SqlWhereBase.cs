using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToleLoB.Sql.Where
{
    internal enum WhereOperator { And, Or }
    public class SqlWhereBase
    {
        internal IList<Tuple<WhereOperator, Expression>> ExpressionList { get; private set; }

        public SqlWhereBase()
        {
            ExpressionList = new List<Tuple<WhereOperator, Expression>>();
        }

        protected SqlWhereBase Set(Expression expression)
        {
            ExpressionList.Clear();
            return And(expression);
        }

        protected SqlWhereBase And(Expression expression)
        {
            ExpressionList.Add(new Tuple<WhereOperator, Expression>(WhereOperator.And, expression));
            return this;
        }
        protected SqlWhereBase Or(Expression expression)
        {
            ExpressionList.Add(new Tuple<WhereOperator, Expression>(WhereOperator.Or, expression));
            return this;
        }
    }
}