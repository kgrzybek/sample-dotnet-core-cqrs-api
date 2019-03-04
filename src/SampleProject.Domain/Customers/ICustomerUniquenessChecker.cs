namespace SampleProject.Domain.Customers
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUnique(Customer customer);
    }
}