using System;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers.Orders
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id);

        Task AddAsync(Customer customer);
    }
}