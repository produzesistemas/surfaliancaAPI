
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Product> GetAll();
        Product Get(int id);
        IQueryable<Product> Where(Expression<Func<Product, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(Product entity);
        void Insert(Product entity);
    }
}
