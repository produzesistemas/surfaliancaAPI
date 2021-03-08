

using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IStoreRepository<T> where T : BaseEntity
    {
        T GetByUser(string id);
        bool CheckExist(string id);
        IQueryable<T> Where(Func<T, bool> expression);

    }
}
