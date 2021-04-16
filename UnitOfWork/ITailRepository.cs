
using Models;

namespace UnitOfWork
{
    public interface ITailRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
