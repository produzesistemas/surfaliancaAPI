
using Models;

namespace UnitOfWork
{
    public interface IPaintRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
