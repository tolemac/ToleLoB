using System;

namespace ToleLoB.Sql.Table
{
    public class SqlTable
    {
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
        }
    }
}