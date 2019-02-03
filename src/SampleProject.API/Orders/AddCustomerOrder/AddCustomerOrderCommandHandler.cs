using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class AddCustomerOrderCommandHandler : IRequestHandler<AddCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public AddCustomerOrderCommandHandler(
            ICustomerRepository customerRepository, 
            IProductRepository productRepository)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
        }

        public async Task<Unit> Handle(AddCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(request.CustomerId);

            var selectedProducts = request.Products.Select(x => new OrderProduct(x.Id, x.Quantity)).ToList();
            var allProducts = await this._productRepository.GetAllAsync();

            var order = new Order(selectedProducts, allProducts);
            
            customer.AddOrder(order);

            await this._customerRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}