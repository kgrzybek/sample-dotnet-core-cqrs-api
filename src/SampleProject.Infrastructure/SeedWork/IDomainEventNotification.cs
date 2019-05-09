using MediatR;

namespace SampleProject.Infrastructure.SeedWork
{
    public interface IDomainEventNotification<out TEventType>
    {
        TEventType DomainEvent { get; }
    }
}