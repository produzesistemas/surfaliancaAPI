
using Models;

namespace UnitOfWork
{
    public interface IShaperRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
