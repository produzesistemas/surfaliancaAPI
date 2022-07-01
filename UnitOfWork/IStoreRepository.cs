using Models;
using System;

namespace UnitOfWork
{
    public interface IStoreRepository : IDisposable
    {
        Store Get();
        void Update(Store entity);
        void Insert(Store entity);
    }
}
