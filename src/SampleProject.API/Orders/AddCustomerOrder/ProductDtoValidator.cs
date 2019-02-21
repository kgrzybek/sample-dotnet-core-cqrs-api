using FluentValidation;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            this.RuleFor(x => x.Currency).Must(x => x == "USD" || x == "EUR")
                .WithMessage("At least one product has invalid currency");
            this.RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("At least one product has invalid quantity");
        }
    }
}