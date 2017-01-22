using System;
using ToleLoB.CQRS.Commands;
using ToleLoB.CQRS.Queries;

namespace ToleLoB.Tests.CQRS
{
    public class Invoice { }
    public class InvoiceDetail { }
    public class Customer { }
    public class Supplier { }
    public class CreateCustomerParameters { }
    public class CreateCustomerCommand : Command<CreateCustomerParameters>
    {
        public CreateCustomerCommand()
        {
            StateItemsAffectedsList.Add(typeof(Customer));
        }
        public override void Execute(CreateCustomerParameters parameters) { }
    }
    public class CreateSupplierParameters { }
    public class CreateSupplierCommand : Command<CreateSupplierParameters>
    {
        public CreateSupplierCommand()
        {
            StateItemsAffectedsList.Add(typeof(Supplier));
        }
        public override void Execute(CreateSupplierParameters parameters) { }
    }
    public class CreateInvoiceParameters { }
    public class CreateInvoiceCommand : Command<CreateInvoiceParameters>
    {
        public CreateInvoiceCommand()
        {
            StateItemsAffectedsList.AddRange(new[] {
                typeof(Invoice),
                typeof(InvoiceDetail),
                typeof(Customer),
                typeof(Supplier)
            });
        }
        public override void Execute(CreateInvoiceParameters parameters) { }
    }

    public class GetCustomersQueryInput { }
    public class GetCustomersQueryOutput { }
    public class GetCustomersQuery : Query<GetCustomersQueryInput, GetCustomersQueryOutput>
    {
        public void SetCacheDuration(TimeSpan duration)
        {
            this.MaxCacheDuration = duration;
        }
        public GetCustomersQuery()
        {
            QueryedStateItemsList.Add(typeof(Customer));
        }
        public override GetCustomersQueryOutput Run(GetCustomersQueryInput parameters)
        {
            return new GetCustomersQueryOutput();
        }
    }
    public class GetSuppliersQueryInput { }
    public class GetSuppliersQueryOutput { }
    public class GetSuppliersQuery : Query<GetSuppliersQueryInput, GetSuppliersQueryOutput>
    {
        public GetSuppliersQuery()
        {
            QueryedStateItemsList.Add(typeof(Supplier));
        }
        public override GetSuppliersQueryOutput Run(GetSuppliersQueryInput parameters)
        {
            return new GetSuppliersQueryOutput();
        }
    }
    public class GetInvoicesQueryInput { }
    public class GetInvoicesQueryOutput { }
    public class GetInvoicesQuery : Query<GetInvoicesQueryInput, GetInvoicesQueryOutput>
    {
        public GetInvoicesQuery()
        {
            QueryedStateItemsList.AddRange(new[] { typeof(Invoice), typeof(InvoiceDetail) });
        }
        public override GetInvoicesQueryOutput Run(GetInvoicesQueryInput parameters)
        {
            return new GetInvoicesQueryOutput();
        }
    }
}