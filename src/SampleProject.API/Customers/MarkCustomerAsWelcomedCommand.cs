using System;
using MediatR;
using SampleProject.Domain.Customers;

namespace SampleProject.API.Customers
{
    public class MarkCustomerAsWelcomedCommand : IRequest
    {
        public MarkCustomerAsWelcomedCommand(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}