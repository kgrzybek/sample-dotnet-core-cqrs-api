using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Infrastructure.Orders
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        internal const string OrdersList = "_orders";
        internal const string OrderProducts = "_orderProducts";

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);
            
            builder.OwnsMany<Order>(OrdersList, x =>
            {
                x.ToTable("Orders", SchemaNames.Orders);
                x.HasForeignKey("CustomerId");
                x.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                x.Property<Guid>(nameof(Order.Id));
                x.HasKey(nameof(Order.Id));

                x.OwnsMany<OrderProduct>(OrderProducts, y =>
                {
                    y.ToTable("OrderProducts", SchemaNames.Orders);
                    y.Property<Guid>("OrderId");
                    y.HasForeignKey("OrderId");
                    y.HasKey("OrderId", "ProductId");

                    y.HasOne(p => p.Product);
                });

                x.OwnsOne<MoneyValue>("_value", y =>
                {
                    y.Property(p => p.Currency).HasColumnName("Currency");
                    y.Property(p => p.Value).HasColumnName("Value");
                });
            });
        }
    }
}