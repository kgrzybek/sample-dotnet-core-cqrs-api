using System;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers.Orders
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);

        Task AddAsync(Customer customer);
    }
}