using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;

namespace OnlineStore.Core.Domain.Categories.Rules;

public class CategoryNameMustBeUniqueRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly ICategoryNameMustBeUniqueChecker _categoryNameMustBeUniqueChecker;

    public CategoryNameMustBeUniqueRule(
        string name, 
        ICategoryNameMustBeUniqueChecker categoryNameMustBeUniqueChecker)
    {
        _name = name;
        _categoryNameMustBeUniqueChecker = categoryNameMustBeUniqueChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await _categoryNameMustBeUniqueChecker.IsUnique(_name, cancellationToken);
        return Check(isUnique);

    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failed($"Category name: '{_name}' must be unique.");
    }
}