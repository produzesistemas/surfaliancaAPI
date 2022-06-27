using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IStringerRepository : IDisposable
    {
        IQueryable<Stringer> GetAll();
        Stringer Get(int id);
        IQueryable<Stringer> Where(Expression<Func<Stringer, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Stringer entity);
        void Insert(Stringer entity);
    }
}
