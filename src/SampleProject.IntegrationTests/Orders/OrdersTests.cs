using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Application.Orders;
using SampleProject.Application.Orders.GetCustomerOrderDetails;
using SampleProject.Application.Orders.GetCustomerOrders;
using SampleProject.Application.Orders.PlaceCustomerOrder;
using SampleProject.Infrastructure.Processing;
using SampleProject.IntegrationTests.SeedWork;

namespace SampleProject.IntegrationTests.Orders
{
    [TestFixture]
    public class OrdersTests : TestBase
    {
        [Test]
        public async Task PlaceOrder_Test()
        {
            var customer = await CommandsExecutor.Execute(new RegisterCustomerCommand("email@email.com", "Sample Customer"));

            List<ProductDto> products = new List<ProductDto>();
            var productId = Guid.Parse("9DB6E474-AE74-4CF5-A0DC-BA23A42E2566");
            products.Add(new ProductDto(productId, 2));
            var orderId = await CommandsExecutor.Execute(new PlaceCustomerOrderCommand(customer.Id, products, "EUR"));

            var orderDetails = await QueriesExecutor.Execute(new GetCustomerOrderDetailsQuery(orderId));

            Assert.That(orderDetails, Is.Not.Null);
            Assert.That(orderDetails.Value, Is.EqualTo(70));
            Assert.That(orderDetails.Products.Count, Is.EqualTo(1));
            Assert.That(orderDetails.Products[0].Quantity, Is.EqualTo(2));
            Assert.That(orderDetails.Products[0].Id, Is.EqualTo(productId));
        }
    }
}