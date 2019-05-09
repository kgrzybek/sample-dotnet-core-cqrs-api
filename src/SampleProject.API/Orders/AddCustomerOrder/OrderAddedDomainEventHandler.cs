using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.Payments;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedEvent>
    {
        private readonly IPaymentRepository _paymentRepository;

        public OrderAddedDomainEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(OrderAddedEvent notification, CancellationToken cancellationToken)
        {
            var newPayment = new Payment(notification.OrderId);

            await this._paymentRepository.AddAsync(newPayment);
        }
    }
}