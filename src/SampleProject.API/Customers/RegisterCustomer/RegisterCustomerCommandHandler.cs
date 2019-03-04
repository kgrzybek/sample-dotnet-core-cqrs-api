using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.API.Customers.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUniquenessChecker _customerUniquenessChecker;

        public RegisterCustomerCommandHandler(
            ICustomerRepository customerRepository, 
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            this._customerRepository = customerRepository;
            _customerUniquenessChecker = customerUniquenessChecker;
        }

        public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.Email, request.Name, this._customerUniquenessChecker);

            await this._customerRepository.AddAsync(customer);

            await this._customerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new CustomerDto { Id = customer.Id };
        }
    }
}