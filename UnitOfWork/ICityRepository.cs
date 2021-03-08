using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface ICityRepository<T> where T : BaseEntity
    {
        T Get(int id);
        T GetByName(string name);
        IQueryable<T> Where(Func<T, bool> expression);
    }
}
