using System;
using MediatR;

namespace SampleProject.API.Customers
{
    public class MarkCustomerAsWelcomedCommand : IRequest
    {
        public MarkCustomerAsWelcomedCommand(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}