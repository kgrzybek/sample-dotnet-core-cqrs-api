using NSubstitute;
using SampleProject.Domain.Customers;

namespace SampleProject.UnitTests.Customers
{
    public class CustomerFactory
    {
        public static Customer Create()
        {
            ICustomerUniquenessChecker customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            string email = "customer@mail.com";
            customerUniquenessChecker.IsUnique(email).Returns(true);

            return Customer.CreateRegistered(email, "Name", customerUniquenessChecker);
        }
    }
}