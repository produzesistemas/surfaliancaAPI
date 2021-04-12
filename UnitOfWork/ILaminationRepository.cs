
using Models;

namespace UnitOfWork
{
    public interface ILaminationRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
