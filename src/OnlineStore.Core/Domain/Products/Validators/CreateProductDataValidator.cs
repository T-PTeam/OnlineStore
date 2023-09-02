using FluentValidation;
using FluentValidation.Results;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Domain.Products.Rules;

namespace OnlineStore.Core.Domain.Products.Validators;

public class CreateProductDataValidator : AbstractValidator<ProductDataCreate>
{
    public CreateProductDataValidator(
        ProductDataCreate product,
        IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker,
        IProductNameMustBeInputChecker productNameMustBeInputChecker)
    {
        RuleFor(product => product.Price)
            .NotNull()
            .GreaterThan(0)
            .CustomAsync(async (price, context, cancellationToken) =>
            {
                if(product != null && product.Price < 0) return;
                var checkResult = await new ProductPriceMustBePositiveRule(price, productPriceMustBePositiveChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Product.Price), error));
                }
            });

        RuleFor(product => product.Name)
            .NotEmpty()
            .NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if(product != null && product.Name == string.Empty) return;
                var check = await new ProductNameMustBeInputRule(name, productNameMustBeInputChecker).CheckAsync(cancellationToken);

                if(check.IsSuccess) return;

                foreach (var error in check.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Product.Name), error));
                }
            });
    }
}