using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Emails;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsSettings _emailsSettings;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public OrderPlacedNotificationHandler(
            IEmailSender emailSender, 
            EmailsSettings emailsSettings, 
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _emailSender = emailSender;
            _emailsSettings = emailsSettings;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Customer].[Email] " +
                               "FROM orders.v_Customers AS [Customer] " +
                               "WHERE [Customer].[Id] = @Id";

            var customerEmail = await connection.QueryFirstAsync<string>(sql, 
                new
                {
                    Id = request.CustomerId.Value
                });

            var emailMessage = new EmailMessage(
                _emailsSettings.FromAddressEmail, 
                customerEmail, 
                OrderNotificationsService.GetOrderEmailConfirmationDescription(request.OrderId));
            
            await _emailSender.SendEmailAsync(emailMessage);
        }
    }
}