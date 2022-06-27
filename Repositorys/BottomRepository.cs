using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class BottomRepository : IBottomRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public BottomRepository(ApplicationDbContext context)
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

        public IQueryable<Bottom> GetAll()
        {
            throw new NotImplementedException();
        }

        public Bottom Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Bottom> Where(Expression<Func<Bottom, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Active(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Bottom entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Bottom entity)
        {
            throw new NotImplementedException();
        }
    }
}
