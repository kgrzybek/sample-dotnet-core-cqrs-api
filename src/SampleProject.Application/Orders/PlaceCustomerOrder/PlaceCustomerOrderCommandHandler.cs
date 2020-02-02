using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IForeignExchange foreignExchange)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            this._foreignExchange = foreignExchange;
        }

        public async Task<Guid> Handle(PlaceCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));
 
            var allProducts = await this._productRepository.GetAllAsync();

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderProductsData = request
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();          
            
            var orderId = customer.PlaceOrder(
                orderProductsData, 
                allProducts, 
                request.Currency,
                conversionRates);

            return orderId.Value;
        }
    }
}