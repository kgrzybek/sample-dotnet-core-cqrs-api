using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SampleProject.API.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            // Send email.
        }
    }
}