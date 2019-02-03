using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.API.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommandHandler : IRequestHandler<ChangeCustomerOrderCommand>
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

            var selectedProducts = request.Products.Select(x => new OrderProduct(x.Id, x.Quantity)).ToList();
            var allProducts = await this._productRepository.GetAllAsync();

            customer.ChangeOrder(request.OrderId, selectedProducts, allProducts);

            await this._customerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}