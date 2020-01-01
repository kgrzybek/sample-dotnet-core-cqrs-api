using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Application.Customers.IntegrationHandlers
{
    public class MarkCustomerAsWelcomedCommandHandler : IRequestHandler<MarkCustomerAsWelcomedCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(command.CustomerId);

            customer.MarkAsWelcomedByEmail();

            return Unit.Value;
        }
    }
}