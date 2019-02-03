using System;
using System.Threading.Tasks;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);

        IUnitOfWork UnitOfWork { get; }
    }
}