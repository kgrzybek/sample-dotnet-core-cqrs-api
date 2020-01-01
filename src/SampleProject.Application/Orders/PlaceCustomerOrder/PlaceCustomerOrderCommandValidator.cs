using FluentValidation;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
    {
        public PlaceCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty");
            RuleForEach(x => x.Products).SetValidator(new ProductDtoValidator());
        }
    }
}