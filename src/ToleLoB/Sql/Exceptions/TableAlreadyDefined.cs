using System;

namespace ToleLoB.Sql.Exceptions
{
    public class TableAlreadyDefined : ArgumentException
    {
        public TableAlreadyDefined(string message) : base(message) { }
    }
}