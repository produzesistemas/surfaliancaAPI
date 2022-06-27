using Models;
using System;
namespace UnitOfWork
{
    public interface IOrderTrackingRepository : IDisposable
    {
        void Insert(OrderTracking entity);
    }
}
