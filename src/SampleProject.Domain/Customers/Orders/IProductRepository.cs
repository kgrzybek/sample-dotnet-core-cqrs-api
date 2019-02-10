using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers.Orders
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetByIdsAsync(List<Guid> ids);
    }
}