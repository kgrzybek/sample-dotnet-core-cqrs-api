using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.API.Orders.ChangeCustomerOrder
{
    internal class ChangeCustomerOrderCommandHandler : IRequestHandler<ChangeCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(request.CustomerId);

            var selectedProducts = await this._productRepository.GetByIdsAsync(request.Products.Select(x => x.Id).ToList());
            var orderCurrency = request.Products.First().Currency;
            var orderProducts = selectedProducts.Select(x => 
                new OrderProduct(
                    x, 
                    request.Products.Single(y => y.Id == x.Id).Quantity,
                    orderCurrency))
                .ToList();

            customer.ChangeOrder(request.OrderId, orderProducts);

            await this._customerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}