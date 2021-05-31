using Models;

namespace UnitOfWork
{
    public interface IBoardModelRepository<T> where T : BaseEntity
    {
        T Get(int id);
        void Update(T entity);
    }
}
