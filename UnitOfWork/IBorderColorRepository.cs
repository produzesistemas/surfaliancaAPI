
using Models;

namespace UnitOfWork
{
    public interface IBorderColorRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
