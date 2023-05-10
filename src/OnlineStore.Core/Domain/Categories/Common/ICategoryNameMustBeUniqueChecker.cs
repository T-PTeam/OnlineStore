namespace OnlineStore.Core.Domain.Categories.Common;

public interface ICategoryNameMustBeUniqueChecker
{
    Task<bool> IsUnique(string name, CancellationToken cancellationToken = default);
}