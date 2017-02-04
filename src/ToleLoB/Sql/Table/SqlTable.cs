using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToleLoB.Sql.Table
{
    public class SqlTable
    {
        internal IList<Expression> PropertyExpression { get; private set; }
        public string SchemaName { get; private set; }
        public string TableName { get; private set; }
        public string Alias { get; private set; }
        public Type EntityType { get; private set; }

        public SqlTable(string schemaName, string tableName, string alias, Type entityType)
        {
            SchemaName = schemaName;
            TableName = tableName;
            Alias = alias;
            EntityType = entityType;
            PropertyExpression = new List<Expression>();
        }
    }

    public class SqlTable<TEntity> : SqlTable
    {
        public SqlTable(string schemaName, string tableName, string alias, Type entityType) : base(schemaName, tableName, alias, entityType)
        {
        }

        public void AddColumn(params Expression<Func<TEntity, object>>[] propertyExpression)
        {
            foreach (var exp in propertyExpression)
            {
                this.PropertyExpression.Add(exp);
            }
        }
    }
}