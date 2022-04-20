
using Models;

namespace UnitOfWork
{
    public interface IPaintRepository<T> where T : BaseEntity
    {
        T Get(int id);
        void Active(int id);
        void Delete(int id);
    }
}
