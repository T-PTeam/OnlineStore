using FluentValidation;
using FluentValidation.Results;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Domain.Products.Rules;

namespace OnlineStore.Core.Domain.Products.Validators;

public class CreateProductDataValidator : AbstractValidator<Product>
{
    public CreateProductDataValidator(
        Product product,
        IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker)
    {
        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(0)
            .CustomAsync(async (price, context, cancellationToken) =>
            {
                var checkResult = await new ProductPriceMustBePositiveRule(price, productPriceMustBePositiveChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Product.Price), error));
                }
            });
    }
}