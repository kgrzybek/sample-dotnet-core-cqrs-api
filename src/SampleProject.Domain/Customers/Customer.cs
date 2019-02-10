using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private readonly List<Order> _orders;

        private Customer()
        {
            this._orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            this._orders.Add(order);

            this.AddDomainEvent(new OrderAddedEvent(order));
        }

        public void ChangeOrder(Guid orderId, List<OrderProduct> products)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(products);

            this.AddDomainEvent(new OrderChangedEvent(order));
        }

        public void RemoveOrder(Guid orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderRemovedEvent(order));
        }
    }
}