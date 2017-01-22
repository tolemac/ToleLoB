using System;
using ToleLoB.DependencyResolver;
using Moq;

namespace ToleLoB.Tests.CQRS
{
    public class CQRSDependencyResolver : IDependencyResolver
    {
        public GetCustomersQuery GetCustomersQuery { get { return GetCustomersQueryMock.Object; } }
        public Mock<GetCustomersQuery> GetCustomersQueryMock { get; set; }
        public GetSuppliersQuery GetSuppliersQuery { get { return GetSuppliersQueryMock.Object; } }
        public Mock<GetSuppliersQuery> GetSuppliersQueryMock { get; set; }
        public GetInvoicesQuery GetInvoicesQuery { get { return GetInvoicesQueryMock.Object; } }
        public Mock<GetInvoicesQuery> GetInvoicesQueryMock { get; set; }
        public CreateCustomerCommand CreateCustomerCommand { get { return CreateCustomerCommandMock.Object; } }
        public Mock<CreateCustomerCommand> CreateCustomerCommandMock { get; set; }
        public CreateInvoiceCommand CreateInvoiceCommand { get { return CreateInvoiceCommandMock.Object; } }
        public Mock<CreateInvoiceCommand> CreateInvoiceCommandMock { get; set; }

        public CQRSDependencyResolver()
        {
            GetCustomersQueryMock = new Mock<GetCustomersQuery>(new object[] { });
            GetCustomersQueryMock.CallBase = true;
            GetSuppliersQueryMock = new Mock<GetSuppliersQuery>(new object[] { });
            GetSuppliersQueryMock.CallBase = true;
            GetInvoicesQueryMock = new Mock<GetInvoicesQuery>(new object[] { });
            GetInvoicesQueryMock.CallBase = true;
            CreateCustomerCommandMock = new Mock<CreateCustomerCommand>(new object[0]);
            CreateCustomerCommandMock.CallBase = true;
            CreateInvoiceCommandMock = new Mock<CreateInvoiceCommand>(new object[0]);
            CreateInvoiceCommandMock.CallBase = true;
        }

        public object Resolve(Type type)
        {
            if (type == typeof(GetCustomersQuery))
                return GetCustomersQuery;
            if (type == typeof(GetSuppliersQuery))
                return GetSuppliersQuery;
            if (type == typeof(GetInvoicesQuery))
                return GetInvoicesQuery;
            if (type == typeof(CreateCustomerCommand))
                return CreateCustomerCommand;
            if (type == typeof(CreateInvoiceCommand))
                return CreateInvoiceCommand;

            return null;
        }
        public TType Resolve<TType>()
        {
            return (TType)Resolve(typeof(TType));
        }
    }
}