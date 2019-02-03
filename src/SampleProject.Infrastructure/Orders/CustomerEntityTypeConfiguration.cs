using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Infrastructure.Orders
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);
            
            builder.OwnsMany<Order>("_orders", x =>
            {
                x.ToTable("Orders", SchemaNames.Orders);
                x.HasForeignKey("CustomerId");
                x.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                x.Property<Guid>(nameof(Order.Id));
                x.Property<decimal>("_value").HasColumnName("Value");
                x.HasKey(nameof(Order.Id));

                x.OwnsMany<OrderProduct>("_orderProducts", y =>
                {
                    y.ToTable("OrderProducts", SchemaNames.Orders);
                    y.Property<Guid>("OrderId");
                    y.HasForeignKey("OrderId");
                    y.HasKey("OrderId", "ProductId");
                });
            });
        }
    }
}