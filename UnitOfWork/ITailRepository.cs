
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ITailRepository : IDisposable
    {
        IQueryable<Tail> GetAll();
        Tail Get(int id);
        IQueryable<Tail> Where(Expression<Func<Tail, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Tail entity);
        void Insert(Tail entity);
    }
}
