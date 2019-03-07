using System;

namespace SampleProject.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public OutboxMessage(DateTime occurredOn, string type, string data)
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = occurredOn;
            this.Type = type;
            this.Data = data;
        }
    }
}