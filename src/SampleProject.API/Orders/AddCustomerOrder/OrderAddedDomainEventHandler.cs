using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders.Events;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedEvent>
    {
        public Task Handle(OrderAddedEvent notification, CancellationToken cancellationToken)
        {
            // Handling logic..

            return Task.CompletedTask;
        }
    }
}