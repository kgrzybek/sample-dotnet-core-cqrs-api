using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Payments;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Payments
{
    internal sealed class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", SchemaNames.Payments);
            
            builder.HasKey(b => b.Id);

            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<OrderId>("_orderId").HasColumnName("OrderId");
            builder.Property("_status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<PaymentStatus, byte>());
            builder.Property<bool>("_emailNotificationIsSent").HasColumnName("EmailNotificationIsSent");
        }
    }
}