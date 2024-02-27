using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers;
using SampleProject.Domain.SeedWork;
using System.Threading.Tasks;
using System.Threading;

namespace SampleProject.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommand : CommandBase<CustomerDto>
    {
        public RegisterCustomerCommand(string email,string name)
        {
            Email = email;
            Name = name;
        }
        public string Email { get; }
        public string Name { get; }

        public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, CustomerDto>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICustomerUniquenessChecker _customerUniquenessChecker;
            private readonly IUnitOfWork _unitOfWork;

            public RegisterCustomerCommandHandler(
                ICustomerRepository customerRepository,
                ICustomerUniquenessChecker customerUniquenessChecker,
                IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _customerUniquenessChecker = customerUniquenessChecker;
                _unitOfWork = unitOfWork;
            }

            public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = Customer.CreateRegistered(request.Email, request.Name, _customerUniquenessChecker);

                await _customerRepository.AddAsync(customer);

                await _unitOfWork.CommitAsync(cancellationToken);

                return new CustomerDto { Id = customer.Id.Value };
            }
        }



    }
}