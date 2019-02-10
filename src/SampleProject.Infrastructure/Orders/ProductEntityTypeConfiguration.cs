using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Infrastructure.Orders
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);

            builder.OwnsOne<MoneyValue>("Price", y =>
            {
                y.Property(p => p.Currency).HasColumnName("PriceCurrency");
                y.Property(p => p.Value).HasColumnName("PriceValue");
            });
        }
    }
}