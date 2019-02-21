using FluentValidation;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class AddCustomerOrderCommandValidator : AbstractValidator<AddCustomerOrderCommand>
    {
        public AddCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty");
            RuleForEach(x => x.Products).SetValidator(new ProductDtoValidator());
        }
    }
}