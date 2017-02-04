using System.Collections.Generic;
using System.Linq;
using ToleLoB.Sql.Exceptions;
using ToleLoB.Sql.Where;
using ToleLoB.Sql.Table;
using System;

namespace ToleLoB.Sql
{
    public class SqlBuilder
    {
        internal SqlTable _mainTable;
        private List<SqlJoin> _joins { get; set; } = new List<SqlJoin>();
        public SqlWhere Where { get; private set; } = new SqlWhere();
        public IReadOnlyList<SqlJoin> Joins { get { return _joins; } }
        public SqlOrder Order { get; set; } = new SqlOrder();

        public SqlTable<TEntity> SetMainTable<TEntity>(string schemaName = null, string tableName = null, string alias = null)
        {
            var result = new SqlTable<TEntity>(schemaName, tableName, alias, typeof(TEntity));
            _mainTable = result;
            return result;
        }

        public SqlJoin<TTableEntity> AddJoin<TTableEntity>(
            string schemaName = null, string tableName = null, string alias = null)
        {
            var entityType = typeof(TTableEntity);
            if (tableName != null && _joins.Any(j => j.SchemaName == schemaName && j.TableName == tableName))
            {
                throw new TableAlreadyDefined($"Table {schemaName}.{tableName} already defined");
            }
            if (alias != null && _joins.Any(j => j.Alias == alias))
            {
                throw new TableAlreadyDefined($"Alias {alias} already defined");
            }
            if (_joins.Any(j => j.Alias == alias && j.SchemaName == schemaName &&
                j.TableName == tableName && j.EntityType == entityType))
            {
                throw new TableAlreadyDefined($"Table {schemaName}.{tableName}, Alias {alias}, EntityType {entityType.FullName} already defined");
            }

            var result = new SqlJoin<TTableEntity>(schemaName, tableName, alias);
            _joins.Add(result);
            return result;
        }

        public SqlJoin Join(Type tableEntityType)
        {
            return _joins.Single(j => j.EntityType == tableEntityType);
        }
        public SqlJoin Join<TTableEntity>()
        {
            return _joins.Single(j => j.EntityType == typeof(TTableEntity));
        }
        public SqlJoin Join<TTableEntity>(string alias)
        {
            return _joins.Single(j => j.EntityType == typeof(TTableEntity) && j.Alias == alias);
        }
        public SqlJoin Join<TTableEntity>(string schemaName, string tableName)
        {
            return _joins.Single(j => j.EntityType == typeof(TTableEntity)
            && j.SchemaName == schemaName && j.TableName == tableName);
        }
    }
}