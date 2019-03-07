using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleProject.Infrastructure.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", SchemaNames.Application);
            
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}