using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;

namespace OnlineStore.Core.Domain.Products.Rules;

public class ProductNameMustBeInputRule : IBusinessRuleAsync
{
    private readonly string _name;
    private readonly IProductNameMustBeInputChecker _productNameMustBeInputChecker;

    public ProductNameMustBeInputRule(string name, IProductNameMustBeInputChecker productNameMustBeInputChecker)
    {
        _name = name;
        _productNameMustBeInputChecker = productNameMustBeInputChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken)
    {
        var isInput = await _productNameMustBeInputChecker.IsInput(_name, cancellationToken);
        return Check(isInput);
    }

    private RuleResult Check(bool isInput)
    {
        if (isInput) return RuleResult.Success();
        return RuleResult.Failed("Product name must be input.");
    }
}