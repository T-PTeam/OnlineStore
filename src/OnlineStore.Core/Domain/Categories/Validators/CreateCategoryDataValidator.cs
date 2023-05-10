﻿using FluentValidation;
using FluentValidation.Results;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;
using OnlineStore.Core.Domain.Categories.Rules;

namespace OnlineStore.Core.Domain.Categories.Validators;

public class CreateCategoryDataValidator : AbstractValidator<Category>
{
    public CreateCategoryDataValidator(
            Category? category,
            ICategoryNameMustBeUniqueChecker categoryNameMustBeUniqueChecker)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .CustomAsync(async (name, context, cancellationToken) =>
            {
                if(category != null && category.Name == name) return;
                var check = await new CategoryNameMustBeUniqueRule(name, categoryNameMustBeUniqueChecker).CheckAsync(cancellationToken);

                if(check.IsSuccess) return;

                foreach (var error in check.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(Category.Name), error));
                }
            });
    }
}