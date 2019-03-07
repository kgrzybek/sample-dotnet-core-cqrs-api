using MediatR;

namespace SampleProject.Infrastructure.SeedWork
{
    public interface IDomainEventNotification<out TEventType> : INotification
    {
        TEventType DomainEvent { get; }
    }
}