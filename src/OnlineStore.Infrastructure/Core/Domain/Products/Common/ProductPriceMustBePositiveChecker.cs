using OnlineStore.Core.Domain.Products.Common;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Common;

public class ProductPriceMustBePositiveChecker : IProductPriceMustBePositiveChecker
{
    public async Task<bool> IsPositiveAsync(decimal price, CancellationToken cancellationToken)
    {
        return price > 0;
    }
}