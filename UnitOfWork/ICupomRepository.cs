using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ICupomRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
