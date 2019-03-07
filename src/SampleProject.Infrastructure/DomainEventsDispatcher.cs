using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using MediatR;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope)
        {
            this._mediator = mediator;
            this._scope = scope;
        }

        public async Task<List<IDomainEventNotification<IDomainEvent>>> DispatchEventsAsync(OrdersContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as SeedWork.IDomainEventNotification<IDomainEvent>);
                }
            }

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            return domainEventNotifications;
        }
    }
}