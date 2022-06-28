using Models;
using System;
using System.Linq;
using UnitOfWork;


namespace Repositorys
{
    public class TypeBlogRepository : ITypeBlogRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public TypeBlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<TypeBlog> GetAll()
        {
            return _context.TypeBlog.AsQueryable();
        }

    }
}
