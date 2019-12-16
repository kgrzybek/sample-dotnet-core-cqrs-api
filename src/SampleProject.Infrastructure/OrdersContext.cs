using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Payments;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Customers;
using SampleProject.Infrastructure.InternalCommands;
using SampleProject.Infrastructure.Outbox;
using SampleProject.Infrastructure.Products;

namespace SampleProject.Infrastructure
{
    public class OrdersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
        }
    }
}
