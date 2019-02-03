using MediatR;

namespace SampleProject.Domain.SeedWork
{
    public interface IDomainEventNotification<out TEventType> : INotification
    {
        TEventType DomainEvent { get; }
    }
}