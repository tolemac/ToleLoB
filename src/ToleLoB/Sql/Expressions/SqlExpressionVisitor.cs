using System;
using System.Linq;
using System.Linq.Expressions;

namespace ToleLoB.Sql.Expressions
{
    public class SqlExpressionVisitor : ExpressionVisitor
    {
        private SqlGenerator _generator;
        public SqlExpressionVisitor(SqlGenerator generator)
        {
            _generator = generator;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _generator.Negate();
                    _generator.OpenGroup();
                    this.Visit(u.Operand);
                    _generator.CloseGroup();
                    break;
                case ExpressionType.Convert:
                    _generator.OpenGroup();
                    this.Visit(u.Operand);
                    _generator.CloseGroup();
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            _generator.OpenGroup();
            this.Visit(b.Left);

            _generator.Operator(b);

            this.Visit(b.Right);
            _generator.CloseGroup();
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            _generator.Constant(c);
            return c;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                _generator.Field(m);
                // sb.Append(m.Member.DeclaringType.Name + ".");
                // sb.Append(m.Member.Name);
                return m;
            }
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Constant)
            {
                var compiled = Expression.Lambda(m).Compile();
                var eval = compiled.DynamicInvoke();
                //sb.Append(eval.ToString());
                var constant = Expression.Constant(eval, m.Type);
                VisitConstant(constant);
                return constant;
            }

            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }

    }
}