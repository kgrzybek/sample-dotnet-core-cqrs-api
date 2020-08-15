using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.Processing.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly OrdersContext _ordersContext;

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, OrdersContext ordersContext)
        {
            _mediator = mediator;
            _scope = scope;
            _ordersContext = ordersContext;
        }

        public async Task DispatchEventsAsync()
        {
            List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity>> domainEntities = _ordersContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            List<IDomainEvent> domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (IDomainEvent domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                Type domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                object domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            IEnumerable<Task> tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            foreach (IDomainEventNotification<IDomainEvent> domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().FullName;
                string data = JsonConvert.SerializeObject(domainEventNotification);
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                _ordersContext.OutboxMessages.Add(outboxMessage);
            }
        }
    }
}