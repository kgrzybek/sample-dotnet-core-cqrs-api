using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules
{
    internal class CustomerEmailValidRule : IBusinessRule
    {
        private readonly ICustomerEmailChecker _customerEmailChecker;

        private readonly string _email;

        public CustomerEmailValidRule(
            ICustomerEmailChecker customerEmailChecker,
            string email)
        {
            _customerEmailChecker = customerEmailChecker;
            _email = email;
        }

        public bool IsBroken()
        {
            return !_customerEmailChecker.IsValid(_email);
        }
        

        public string Message => "Customer with this email already exists.";
    }
}