using ToleLoB.CQRS.Queries;
using Xunit;
using Moq;
using ToleLoB.CQRS.Commands;

namespace ToleLoB.Tests.CQRS
{
    public class CQRSSynchroTests
    {
        [Fact]
        public void CommandInvalidateQueryCacheOfQueriesThatUseTheSameStateItems()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver);
            var commandSystem = new CommandSystem(resolver, querySystem);
            var customerQueryParameters = new GetCustomersQueryInput();
            var customerCommandParameters = new CreateCustomerParameters();

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerQueryParameters);
            commandSystem.Execute<CreateCustomerCommand, CreateCustomerParameters>(customerCommandParameters);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerQueryParameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(customerQueryParameters), Times.Exactly(2));
        }

        [Fact]
        public void ACommandInvalidateQueryCacheOfSeveralQueriesThatUseTheSameStateItems()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver);
            var commandSystem = new CommandSystem(resolver, querySystem);
            var customerParameters = new GetCustomersQueryInput();
            var supplierParameters = new GetSuppliersQueryInput();
            var invoiceParameters = new GetInvoicesQueryInput();
            var createInvoiceParameters = new CreateInvoiceParameters();

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerParameters);
            querySystem.Run<GetSuppliersQuery, GetSuppliersQueryInput, GetSuppliersQueryOutput>(supplierParameters);
            querySystem.Run<GetInvoicesQuery, GetInvoicesQueryInput, GetInvoicesQueryOutput>(invoiceParameters);

            commandSystem.Execute<CreateInvoiceCommand, CreateInvoiceParameters>(createInvoiceParameters);

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerParameters);
            querySystem.Run<GetSuppliersQuery, GetSuppliersQueryInput, GetSuppliersQueryOutput>(supplierParameters);
            querySystem.Run<GetInvoicesQuery, GetInvoicesQueryInput, GetInvoicesQueryOutput>(invoiceParameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(customerParameters), Times.Exactly(2));
            resolver.GetSuppliersQueryMock.Verify(f => f.Run(supplierParameters), Times.Exactly(2));
            resolver.GetInvoicesQueryMock.Verify(f => f.Run(invoiceParameters), Times.Exactly(2));
        }
    }
}