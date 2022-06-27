using Models;
using System;
using System.Linq;
namespace UnitOfWork
{
    public interface IProductStatusRepository : IDisposable
    {
        IQueryable<ProductStatus> GetAll();
    }

}
