using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface IShippingCompanyRepository : IDisposable
    {
        IQueryable<ShippingCompany> GetAll();
        ShippingCompany Get(int id);
        IQueryable<ShippingCompany> Where(Expression<Func<ShippingCompany, bool>> expression);
        void Active(int id);
        void Delete(int id);
        void Update(ShippingCompany entity);
        void Insert(ShippingCompany entity);
    }
}
