using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IProductTypeRepository : IDisposable
    {
        IQueryable<ProductType> GetAll();
    }
}
