using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;

namespace SampleProject.API.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : IRequestHandler<PlaceCustomerOrderCommand>
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

        public async Task<Unit> Handle(PlaceCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));
 
            var selectedProducts = await this._productRepository.GetByIdsAsync(request.Products.Select(x => new ProductId(x.Id)).ToList());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderProducts = selectedProducts.Select(x =>
                new OrderProduct(
                    x, 
                    request.Products.Single(y => y.Id == x.Id.Value).Quantity,
                    request.Products.Single(y => y.Id == x.Id.Value).Currency,
                    conversionRates)
                ).ToList();
            
            var order = new Order(orderProducts);
            
            customer.PlaceOrder(order);

            return Unit.Value;
        }
    }
}