using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrdersContext _context;

        public CustomerRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await this._context.Customers
                .IncludePaths(
                    CustomerEntityTypeConfiguration.OrdersList, 
                    CustomerEntityTypeConfiguration.OrderProducts, 
                    nameof(Product))
                .SingleAsync(x => x.Id == id);
        }
    }
}