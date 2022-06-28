using Models;
using System;
using System.Linq;
namespace UnitOfWork
{
    public interface ITypeBlogRepository : IDisposable
    {
        IQueryable<TypeBlog> GetAll();
    }

}
