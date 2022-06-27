using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ILaminationRepository : IDisposable
    {
        IQueryable<Lamination> GetAll();
        Lamination Get(int id);
        IQueryable<Lamination> Where(Expression<Func<Lamination, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Lamination entity);
        void Insert(Lamination entity);
    }
}
