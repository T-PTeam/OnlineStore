using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;

namespace OnlineStore.Core.Domain.Products.Rules;

public class ProductPriceMustBePositiveRule : IBusinessRuleAsync
{
    private readonly decimal _price;
    private readonly IProductPriceMustBePositiveChecker _priceMustBePositiveChecker;

    public ProductPriceMustBePositiveRule(
        decimal price,
        IProductPriceMustBePositiveChecker priceMustBePositiveChecker)
    {
        _price = price;
        _priceMustBePositiveChecker = priceMustBePositiveChecker;
    }

    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isPositive = await _priceMustBePositiveChecker.IsPositiveAsync(_price, cancellationToken);
        return Check(isPositive);
    }

    private RuleResult Check(bool isPositive)
    {
        if (isPositive) return RuleResult.Success();
        return RuleResult.Failed($"Product price: '{_price}' must be > 0.");
    }
}