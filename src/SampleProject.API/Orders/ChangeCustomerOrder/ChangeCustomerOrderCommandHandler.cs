using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;

namespace SampleProject.API.Orders.ChangeCustomerOrder
{
    internal class ChangeCustomerOrderCommandHandler : IRequestHandler<ChangeCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IForeignExchange _foreignExchange;

        public ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IForeignExchange foreignExchange)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            _foreignExchange = foreignExchange;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(request.CustomerId);

            var selectedProducts = await this._productRepository.GetByIdsAsync(request.Products.Select(x => x.Id).ToList());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderCurrency = request.Products.First().Currency;
            var orderProducts = selectedProducts.Select(x => 
                new OrderProduct(
                    x, 
                    request.Products.Single(y => y.Id == x.Id).Quantity,
                    orderCurrency,
                    conversionRates))
                .ToList();

            customer.ChangeOrder(request.OrderId, orderProducts, conversionRates);

            return Unit.Value;
        }
    }
}