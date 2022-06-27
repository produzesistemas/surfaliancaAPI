using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IBlogRepository : IDisposable
    {
        Blog Get(int id);
        IQueryable<Blog> Where(Expression<Func<Blog, bool>> expression);
        void Delete(int id);
        void Update(Blog entity);
        void Insert(Blog entity);

        IQueryable<Blog> GetAll();
    }
}
