using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        public string Email { get; private set; }

        public string Name { get; private set; }

        private readonly List<Order> _orders;

        private bool _welcomeEmailWasSent;

        private Customer()
        {
            this._orders = new List<Order>();
        }

        public Customer(string email, string name, ICustomerUniquenessChecker customerUniquenessChecker)
        {
            this.Id = new CustomerId(Guid.NewGuid());
            this.Email = email;
            this.Name = name;
            this._welcomeEmailWasSent = false;

            var isUnique = customerUniquenessChecker.IsUnique(this);
            if (!isUnique)
            {
                throw new BusinessRuleValidationException("Customer with this email already exists.");
            }

            this.AddDomainEvent(new CustomerRegisteredEvent(this));
        }

        public void AddOrder(Order order)
        {
            if (this._orders.Count(x => x.IsOrderedToday()) >= 2)
            {
                throw new BusinessRuleValidationException("You cannot order more than 2 orders on the same day");
            }

            this._orders.Add(order);

            this.AddDomainEvent(new OrderAddedEvent(order.Id));
        }

        public void ChangeOrder(
            OrderId orderId, 
            List<Product> existingProducts,
            List<OrderProduct> newOrderProducts,
            List<ConversionRate> conversionRates)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(existingProducts, newOrderProducts, conversionRates);

            this.AddDomainEvent(new OrderChangedEvent(order));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderRemovedEvent(order));
        }

        public void MarkAsWelcomedByEmail()
        {
            this._welcomeEmailWasSent = true;
        }

        public List<ProductId> GetOrderProductsIds(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);

            return order.GetProductsIds();
        }
    }
}