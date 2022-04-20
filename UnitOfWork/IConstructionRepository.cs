using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IConstructionRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IQueryable<T> Where(Func<T, bool> expression);
        void Active(int id);
        void Delete(int id);
    }
}
