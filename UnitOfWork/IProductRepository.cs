
using Models;

namespace UnitOfWork
{
    public interface IProductRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
