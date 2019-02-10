using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Infrastructure.Orders
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext _context;
        public ProductRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await this._context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return await this._context
                .Products
                .Include(x => x.Price)
                .Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}