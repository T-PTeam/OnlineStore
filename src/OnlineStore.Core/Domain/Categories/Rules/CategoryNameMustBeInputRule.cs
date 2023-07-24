using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;

namespace OnlineStore.Core.Domain.Categories.Rules;

public class CategoryNameMustBeInputRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly ICategoryNameMustBeInputChecker _categoryNameMustBeInputChecker;

    public CategoryNameMustBeInputRule(
        string name, 
        ICategoryNameMustBeInputChecker categoryNameMustBeInputChecker)
    {
        _name = name;
        _categoryNameMustBeInputChecker = categoryNameMustBeInputChecker;
    }


    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isInput = await _categoryNameMustBeInputChecker.IsInput(_name, cancellationToken);
        return Check(isInput);
    }

    private RuleResult Check(bool isInput)
    {
        if(isInput) return RuleResult.Success();
        return RuleResult.Failed($"Category name: '{_name}' must be input.");
    }
}