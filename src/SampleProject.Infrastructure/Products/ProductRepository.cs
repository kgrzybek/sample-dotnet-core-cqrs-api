using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Products;

namespace SampleProject.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext _context;
        public ProductRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return await this._context
                .Products
                .Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}