using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers;
using System;

namespace SampleProject.Application.Customers
{
    public class MarkCustomerAsWelcomedCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : base(id)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}