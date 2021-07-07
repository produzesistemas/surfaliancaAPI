

using Models;

namespace UnitOfWork
{
    public interface IOrderRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
