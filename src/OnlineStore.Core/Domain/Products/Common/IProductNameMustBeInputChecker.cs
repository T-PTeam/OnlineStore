namespace OnlineStore.Core.Domain.Products.Common;

public interface IProductNameMustBeInputChecker
{
    Task<bool> IsInput(string name, CancellationToken cancellationToken);
}