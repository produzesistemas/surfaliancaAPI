

using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IStoreRepository<T> where T : BaseEntity
    {
        IQueryable<T> Where(Func<T, bool> expression);

    }
}
