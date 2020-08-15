using NUnit.Framework;
using SampleProject.Application.Customers.GetCustomerDetails;
using SampleProject.Application.Customers.IntegrationHandlers;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.Processing;
using SampleProject.IntegrationTests.SeedWork;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SampleProject.IntegrationTests.Customers
{
    [TestFixture]
    public class CustomersTests : TestBase
    {
        [Test]
        public async Task RegisterCustomerTest()
        {
            const string email = "newCustomer@mail.com";
            const string name = "Sample Company";

            Application.Customers.CustomerDto customer = await CommandsExecutor.Execute(new RegisterCustomerCommand(email, name));
            CustomerDetailsDto customerDetails = await QueriesExecutor.Execute(new GetCustomerDetailsQuery(customer.Id));

            Assert.That(customerDetails, Is.Not.Null);
            Assert.That(customerDetails.Name, Is.EqualTo(name));
            Assert.That(customerDetails.Email, Is.EqualTo(email));

            SqlConnection connection = new SqlConnection(ConnectionString);
            System.Collections.Generic.List<Infrastructure.Processing.Outbox.OutboxMessageDto> messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);

            Assert.That(messagesList.Count, Is.EqualTo(1));

            CustomerRegisteredNotification customerRegisteredNotification =
                OutboxMessagesHelper.Deserialize<CustomerRegisteredNotification>(messagesList[0]);

            Assert.That(customerRegisteredNotification.CustomerId, Is.EqualTo(new CustomerId(customer.Id)));
        }
    }
}