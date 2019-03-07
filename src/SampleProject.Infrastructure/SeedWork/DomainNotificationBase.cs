namespace SampleProject.Infrastructure.SeedWork
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T>
    {
        public T DomainEvent { get; }

        public DomainNotificationBase(T domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}