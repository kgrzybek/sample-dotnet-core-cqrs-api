namespace SampleProject.Domain.Customers
{
    public interface ICustomerEmailChecker
    {
        bool IsValid(string customerEmail);
    }
}