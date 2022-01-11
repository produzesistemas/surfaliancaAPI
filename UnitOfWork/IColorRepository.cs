
using Models;

namespace UnitOfWork
{
    public interface IColorRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
