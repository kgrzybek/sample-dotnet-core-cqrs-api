using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IForeignExchange foreignExchange,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _customerRepository = customerRepository;
            _foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            Customer customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

            System.Collections.Generic.List<ProductPriceData> allProductPrices =
                await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            System.Collections.Generic.List<ConversionRate> conversionRates = _foreignExchange.GetConversionRates();

            System.Collections.Generic.List<OrderProductData> orderProductsData = command
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();

            OrderId orderId = customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                command.Currency,
                conversionRates);

            return orderId.Value;
        }
    }
}