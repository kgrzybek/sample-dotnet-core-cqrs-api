using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Products;
using SampleProject.Domain.SharedKernel;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Customers
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        internal const string OrdersList = "_orders";
        internal const string OrderProducts = "_orderProducts";

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);

            builder.Property<bool>("_welcomeEmailWasSent").HasColumnName("WelcomeEmailWasSent");
            
            builder.OwnsMany<Order>(OrdersList, x =>
            {
                x.ToTable("Orders", SchemaNames.Orders);
                x.HasForeignKey("CustomerId");
                x.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                x.Property<DateTime>("_orderDate").HasColumnName("OrderDate");
                x.Property<DateTime?>("_orderChangeDate").HasColumnName("OrderChangeDate");
                x.Property<OrderId>("Id");
                x.HasKey("Id");

                x.Property("_status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<OrderStatus, byte>());

                x.OwnsMany<OrderProduct>(OrderProducts, y =>
                {
                    y.ToTable("OrderProducts", SchemaNames.Orders);
                    y.Property<OrderId>("OrderId");
                    y.Property<ProductId>("ProductId");
                    y.HasForeignKey("OrderId");
                    y.HasKey("OrderId", "ProductId");

                    y.OwnsOne<MoneyValue>("Value", mv =>
                    {
                        mv.Property(p => p.Currency).HasColumnName("Currency");
                        mv.Property(p => p.Value).HasColumnName("Value");
                    });

                    y.OwnsOne<MoneyValue>("ValueInEUR", mv =>
                    {
                        mv.Property(p => p.Currency).HasColumnName("CurrencyEUR");
                        mv.Property(p => p.Value).HasColumnName("ValueInEUR");
                    });
                });

                x.OwnsOne<MoneyValue>("_value", y =>
                {
                    y.Property(p => p.Currency).HasColumnName("Currency");
                    y.Property(p => p.Value).HasColumnName("Value");
                });

                x.OwnsOne<MoneyValue>("_valueInEUR", y =>
                {
                    y.Property(p => p.Currency).HasColumnName("CurrencyEUR");
                    y.Property(p => p.Value).HasColumnName("ValueInEUR");
                });
            });
        }
    }
}