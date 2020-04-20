using NSubstitute;
using SampleProject.Domain.Customers;

namespace SampleProject.UnitTests.Customers
{
    public class CustomerFactory
    {
        public static Customer Create()
        {
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            var email = "customer@mail.com";
            customerUniquenessChecker.IsUnique(email).Returns(true);

            return Customer.CreateRegistered(email, "Name", customerUniquenessChecker);
        }
    }
}