using MediatR;

namespace SampleProject.API.Customers.RegisterCustomer
{
    public class RegisterCustomerCommand : IRequest<CustomerDto>
    {
        public string Email { get; }

        public string Name { get; }

        public RegisterCustomerCommand(string email, string name)
        {
            this.Email = email;
            this.Name = name;
        }      
    }
}