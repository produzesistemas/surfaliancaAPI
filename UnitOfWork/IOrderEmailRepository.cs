using Models;
using System;

namespace UnitOfWork
{
    public interface IOrderEmailRepository : IDisposable
    {
        void Insert(OrderEmail entity);
    }
}
