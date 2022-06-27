using Models;
using System;

namespace UnitOfWork
{
    public interface IShippingCompanyStateRepository : IDisposable
    {
        void Delete(int id);
        void Insert(ShippingCompanyState entity);
    }
}
