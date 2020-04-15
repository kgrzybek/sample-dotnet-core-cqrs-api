using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules
{
    public class CustomerEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly ICustomerUniquenessChecker _customerUniquenessChecker;

        private readonly string _email;

        public CustomerEmailMustBeUniqueRule(
            ICustomerUniquenessChecker customerUniquenessChecker, 
            string email)
        {
            _customerUniquenessChecker = customerUniquenessChecker;
            _email = email;
        }

        public bool IsBroken() => !_customerUniquenessChecker.IsUnique(_email);

        public string Message => "Customer with this email already exists.";
    }
}