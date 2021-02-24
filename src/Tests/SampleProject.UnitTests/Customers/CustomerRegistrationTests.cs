using NSubstitute;
using NUnit.Framework;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Rules;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.Customers
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public void GivenCustomerEmailIsUnique_WhenCustomerIsRegistering_IsSuccessful()
        {
            // Arrange
            ICustomerUniquenessChecker customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(true);

            // Act
            Customer customer = Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker);

            // Assert
            AssertPublishedDomainEvent<CustomerRegisteredEvent>(customer);
        }

        [Test]
        public void GivenCustomerEmailIsNotUnique_WhenCustomerIsRegistering_BreaksCustomerEmailMustBeUniqueRule()
        {
            // Arrange
            ICustomerUniquenessChecker customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(false);

            // Assert
            AssertBrokenRule<CustomerEmailMustBeUniqueRule>(() =>
            {
                // Act
                Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker);
            });
        }
    }
}