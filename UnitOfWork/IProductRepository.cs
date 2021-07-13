
using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IProductRepository<T> where T : BaseEntity
    {
        T Get(int id);

        IQueryable<T> GetPromotionSpotlight();
        IQueryable<T> GetByType(int id);
    }
}
