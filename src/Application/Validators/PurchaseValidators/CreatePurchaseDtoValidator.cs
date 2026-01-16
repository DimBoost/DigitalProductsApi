using DigitalProductsApi.src.Application.DTOs.Purchases;
using FluentValidation;

namespace DigitalProductsApi.src.Application.Validators.PurchaseValidators
{
    public class CreatePurchaseDtoValidator : AbstractValidator<CreatePurchaseDto>
    {
        public CreatePurchaseDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
        }
    }
}
