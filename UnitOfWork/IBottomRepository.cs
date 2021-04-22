
using Models;

namespace UnitOfWork
{
    public interface IBottomRepository<T> where T : BaseEntity
    {
        T Get(int id);
    }
}
