namespace OnlineStore.Core.Common;

public interface IUnitOfWork
{
    void SaveChanges();
}