using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ToleLoB.Sql.Table;

namespace ToleLoB.Sql
{
    public class SqlGenerator
    {
        SqlBuilder _builder;
        public SqlGenerator(SqlBuilder builder)
        {
            _builder = builder;
        }

        private StringBuilder sb = new StringBuilder();
        public virtual void Clear()
        {
            sb.Clear();
        }
        public virtual void Field(MemberExpression exp)
        {
            string tableName, fieldName;
            SqlJoin joinTable;
            if (_builder._mainTable.EntityType == exp.Member.DeclaringType)
            {
                tableName = _builder._mainTable.TableName ?? exp.Member.DeclaringType.Name;
            }
            else if ((joinTable = _builder.Join(exp.Member.DeclaringType)) != null)
            {
                tableName = joinTable.TableName ?? exp.Member.DeclaringType.Name;
            }
            else
            {
                tableName = exp.Member.DeclaringType.Name;
            }
            fieldName = exp.Member.Name;

            sb.Append()
        }

        protected bool IsNullConstant(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null);
        }

        public virtual void Operator(BinaryExpression exp)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.And:
                    //sb.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    //sb.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    //sb.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    //sb.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(exp.Right))
                    {
                        //sb.Append(" IS ");
                    }
                    else
                    {
                        //sb.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(exp.Right))
                    {
                        //sb.Append(" IS NOT ");
                    }
                    else
                    {
                        //sb.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    //sb.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    //sb.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    //sb.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    //sb.Append(" >= ");
                    break;

                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", exp.NodeType));

            }
        }

        public virtual void Constant(ConstantExpression exp)
        {
            IQueryable q = exp.Value as IQueryable;

            if (q == null && exp.Value == null)
            {
                //sb.Append("NULL");
            }
            else if (q == null)
            {
                switch (Type.GetTypeCode(exp.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        //sb.Append(((bool)exp.Value) ? 1 : 0);
                        break;

                    case TypeCode.String:
                        // sb.Append("'");
                        // sb.Append(exp.Value);
                        // sb.Append("'");
                        break;

                    case TypeCode.DateTime:
                        // sb.Append("'");
                        // sb.Append(exp.Value);
                        // sb.Append("'");
                        break;

                    case TypeCode.Object:
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", exp.Value));

                    default:
                        //sb.Append(exp.Value);
                        break;
                }
            }
        }

        public virtual void Null()
        {

        }
        public virtual void OpenGroup()
        {
        }

        public virtual void CloseGroup()
        {
        }

        public virtual void Negate()
        {
        }
    }
}