using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface ITeamRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IQueryable<T> Where(Func<T, bool> expression);
    }
}
