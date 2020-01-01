using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Configuration.Processing;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Payments;
using SampleProject.Domain.Products;
using SampleProject.Infrastructure.Domain.Customers;
using SampleProject.Infrastructure.Domain.Payments;
using SampleProject.Infrastructure.Domain.Products;
using SampleProject.Infrastructure.Processing.InternalCommands;
using SampleProject.Infrastructure.Processing.Outbox;

namespace SampleProject.Infrastructure.Database
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
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}