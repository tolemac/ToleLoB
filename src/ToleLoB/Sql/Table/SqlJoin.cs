using System;
using ToleLoB.Sql.Where;

namespace ToleLoB.Sql.Table
{
    public class SqlJoin : SqlTable
    {
        public SqlWhereBase Condition { get; protected set; }
        public SqlJoin(string schemaName, string tableName, string alias, Type entityType)
            : base(schemaName, tableName, alias, entityType)
        {
            Condition = new SqlWhere();
        }
    }
    public class SqlJoin<TMainEntity> : SqlJoin
    {
        public new SqlWhere<TMainEntity> Condition { get; private set; }
        public SqlJoin(string schemaName, string tableName, string alias)
            : base(schemaName, tableName, alias, typeof(TMainEntity))
        {
            base.Condition = (SqlWhereBase)(Condition = new SqlWhere<TMainEntity>());
        }
    }
}