using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Customers.IntegrationHandlers
{
    public class MarkCustomerAsWelcomedCommandHandler : ICommandHandler<MarkCustomerAsWelcomedCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        public MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
        {
            Domain.Customers.Customer customer = await _customerRepository.GetByIdAsync(command.CustomerId);

            customer.MarkAsWelcomedByEmail();

            return Unit.Value;
        }
    }
}