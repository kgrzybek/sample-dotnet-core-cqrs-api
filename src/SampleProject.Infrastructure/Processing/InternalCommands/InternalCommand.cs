using System;

namespace SampleProject.Infrastructure.Processing.InternalCommands
{
    public class InternalCommand
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }
    }
}