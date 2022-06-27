using Models;
using System;
using System.Linq;
namespace UnitOfWork
{
    public interface IStateRepository : IDisposable
    {
        IQueryable<State> GetAll();
    }

}
