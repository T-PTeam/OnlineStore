namespace OnlineStore.Core.Domain.Categories.Common;

public interface ICategoryNameMustBeInputChecker
{
    Task<bool> IsInput(string name, CancellationToken cancellationToken);
}