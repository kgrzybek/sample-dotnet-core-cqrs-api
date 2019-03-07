using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Customers;
using SampleProject.Infrastructure.Outbox;
using SampleProject.Infrastructure.Products;

namespace SampleProject.Infrastructure
{
    public class OrdersContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IMediator _mediator;

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        public OrdersContext(DbContextOptions<OrdersContext> options, IDomainEventsDispatcher domainEventsDispatcher, IMediator mediator) : base(options)
        {
            this._domainEventsDispatcher = domainEventsDispatcher;
            this._mediator = mediator;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var notifications = await this._domainEventsDispatcher.DispatchEventsAsync(this);

            foreach (var domainEventNotification in notifications)
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification);
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                this.OutboxMessages.Add(outboxMessage);
            }

            var saveResult = await base.SaveChangesAsync(cancellationToken);

            return saveResult;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
    }
}