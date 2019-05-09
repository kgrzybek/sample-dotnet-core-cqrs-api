using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class AddCustomerOrderCommandHandler : IRequestHandler<AddCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IForeignExchange _foreignExchange;

        public AddCustomerOrderCommandHandler(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository, 
            IForeignExchange foreignExchange)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            this._foreignExchange = foreignExchange;
        }

        public async Task<Unit> Handle(AddCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(request.CustomerId);
 
            var selectedProducts = await this._productRepository.GetByIdsAsync(request.Products.Select(x => x.Id).ToList());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderProducts = selectedProducts.Select(x =>
                new OrderProduct(
                    x, 
                    request.Products.Single(y => y.Id == x.Id).Quantity,
                    request.Products.Single(y => y.Id == x.Id).Currency,
                    conversionRates)
                ).ToList();
            
            var order = new Order(orderProducts);
            
            customer.AddOrder(order);

            return Unit.Value;
        }
    }
}