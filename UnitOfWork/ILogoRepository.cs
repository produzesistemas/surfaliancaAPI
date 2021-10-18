
using Models;

namespace UnitOfWork
{
    public interface ILogoRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
