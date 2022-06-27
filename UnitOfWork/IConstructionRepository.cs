using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IConstructionRepository : IDisposable
    { 
        IQueryable<Construction> GetAll();
        Construction Get(int id);
        IQueryable<Construction> Where(Expression<Func<Construction, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Construction entity);
        void Insert(Construction entity);
    }
}
