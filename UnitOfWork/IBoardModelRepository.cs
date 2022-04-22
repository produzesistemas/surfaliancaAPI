using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IBoardModelRepository<T> where T : BaseEntity
    {
        T Get(int id);
        void Update(T entity);

        IQueryable<T> Where(Func<T, bool> expression);
        void Active(int id);
        void Delete(int id);
    }
}
