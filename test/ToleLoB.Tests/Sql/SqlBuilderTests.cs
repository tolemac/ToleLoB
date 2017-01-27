using Xunit;
using System;
using ToleLoB.Sql;

namespace ToleLoB.Tests.Sql
{
    public class SqlBuilderTests
    {
        [Fact]
        public void CanRegisterMainTableJoinsWhereAndOrder()
        {
            var b = new SqlBuilder();

            b.SetMainTable<Invoice>("WH", "Invoices", "T1");
            b.AddJoin<InvoiceDetail>("WH", "InvoiceDetail", "T2")
                .Condition.Set<Invoice>((d, i) => i.Id == d.InvoiceId && d.Price > 0 && i.Total > 0);
            b.AddJoin<Customer>("WH", "Customer", "T3")
                .Condition.Set<Invoice>((c, i) => i.CustomerId == c.Id).And(c => !c.Disable);
            b.Where.Set<Invoice, Customer>((i, c) => i.Serie == "B" && c.Id == 234);
            b.Order.Ascend<Customer>(c => c.Id).Descend<Invoice>(i => i.Date);

            Assert.NotNull(b._mainTable);
            Assert.Equal(b.Joins.Count, 2);
            Assert.Equal(b.Where.ExpressionList.Count, 1);
            Assert.Equal(b.Order.ExpressionList.Count, 2);
            Assert.Equal(b.Join<InvoiceDetail>().Condition.ExpressionList.Count, 1);
            Assert.Equal(b.Join<Customer>().Condition.ExpressionList.Count, 2);
        }

    }
}