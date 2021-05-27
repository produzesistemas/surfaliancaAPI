using Models;

namespace UnitOfWork
{
    public interface IStoreRepository<T> where T : BaseEntity
    {
        T Get();
        T GetToIndex();

    }
}
