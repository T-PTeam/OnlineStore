namespace OnlineStore.Core.Domain.Products.Common;

public interface IProductPriceMustBePositiveChecker
{
    Task<bool> IsPositiveAsync(decimal price, CancellationToken cancellationToken);
}