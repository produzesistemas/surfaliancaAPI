using Models;
using System;
using System.Linq;

namespace UnitOfWork
{
    public interface IStringerRepository<T> where T : BaseEntity
    {
        void Active(int id);
        void Delete(int id);
    }
}
