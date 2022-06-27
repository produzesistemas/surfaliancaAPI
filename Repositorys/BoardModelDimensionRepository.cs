using Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorys
{
    public class BoardModelDimensionsRepository : IBoardModelDimensionsRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public BoardModelDimensionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<BoardModelDimensions> GetAll()
        {
            return _context.BoardModelDimensions.AsQueryable();
        }

        public void Delete(int id)
        {
            var entity = _context.BoardModelDimensions.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public BoardModelDimensions Get(int id)
        {
            return _context.BoardModelDimensions.Single(b => b.Id == id);
        }

        public void Update(BoardModelDimensions entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<BoardModelDimensions> Where(Expression<Func<BoardModelDimensions, bool>> expression)
        {
            return _context.BoardModelDimensions.Where(expression).AsQueryable();
        }

        public void Insert(BoardModelDimensions entity)
        {
            _context.BoardModelDimensions.Add(entity);
            _context.SaveChanges();
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
    }
}
