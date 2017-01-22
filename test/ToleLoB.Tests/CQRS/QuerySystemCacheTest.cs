using ToleLoB.CQRS.Queries;
using Xunit;
using Moq;
using System;

namespace ToleLoB.Tests.CQRS
{
    public class QuerySystemCacheTest
    {
        [Fact]
        public void QuerySystemUseCache()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver);
            var parameters = new GetCustomersQueryInput();

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(parameters), Times.Once);
        }

        [Fact]
        public void QuerySystemUseCacheWithSeveralQueries()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver);
            var customerParameters = new GetCustomersQueryInput();
            var supplierParameters = new GetSuppliersQueryInput();
            var invoiceParameters = new GetInvoicesQueryInput();

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerParameters);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(customerParameters);
            querySystem.Run<GetSuppliersQuery, GetSuppliersQueryInput, GetSuppliersQueryOutput>(supplierParameters);
            querySystem.Run<GetSuppliersQuery, GetSuppliersQueryInput, GetSuppliersQueryOutput>(supplierParameters);
            querySystem.Run<GetInvoicesQuery, GetInvoicesQueryInput, GetInvoicesQueryOutput>(invoiceParameters);
            querySystem.Run<GetInvoicesQuery, GetInvoicesQueryInput, GetInvoicesQueryOutput>(invoiceParameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(customerParameters), Times.Once);
            resolver.GetSuppliersQueryMock.Verify(f => f.Run(supplierParameters), Times.Once);
            resolver.GetInvoicesQueryMock.Verify(f => f.Run(invoiceParameters), Times.Once);
        }

        [Fact]
        public void QueryPurgeResultAfterQueryMaxCacheDurationTimeSpan()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver);
            var parameters = new GetCustomersQueryInput();
            resolver.GetCustomersQuery.SetCacheDuration(TimeSpan.FromMilliseconds(200));

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);
            System.Threading.Thread.Sleep(201);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(parameters), Times.Exactly(2));
        }

        [Fact]
        public void QuerySystemPurgeResultAfterQuerySystemMaxCacheDurationTimeSpan()
        {
            var resolver = new CQRSDependencyResolver();
            var querySystem = new QuerySystem(resolver, TimeSpan.FromMilliseconds(200));
            var parameters = new GetCustomersQueryInput();

            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);
            System.Threading.Thread.Sleep(201);
            querySystem.Run<GetCustomersQuery, GetCustomersQueryInput, GetCustomersQueryOutput>(parameters);

            resolver.GetCustomersQueryMock.Verify(f => f.Run(parameters), Times.Exactly(2));
        }
    }
}