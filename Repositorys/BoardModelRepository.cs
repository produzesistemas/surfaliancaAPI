using Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Repositorys
{
    public class BoardModelRepository : IBoardModelRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<BoardModel> GetAll()
        {
            return _context.BoardModel.AsQueryable();
        }

        public void Active(int id)
        {
            var entity = _context.BoardModel.Single(x => x.Id == id);
            if (entity.Active)
            {
                entity.Active = false;
            }
            else
            {
                entity.Active = true;
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            if (_context.OrderProductOrdered.Any(c => c.BoardModelId == id))
            {
                throw new Exception("O modelo não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            };

            var dimensions = _context.BoardModelDimensions.Where(c => c.BoardModelId == id);
            _context.RemoveRange(dimensions);
            var entity = _context.BoardModel.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public BoardModel Get(int id)
        {
               return _context.BoardModel
                .Include(c => c.BoardModelDimensions)
                .Include(c => c.BoardModelBottoms)
                .Include(c => c.BoardModelConstructions)
                .Include(c => c.BoardModelFinSystems)
                .Include(c => c.BoardModelLaminations)
                .Include(c => c.BoardModelStringers)
                .Include(c => c.BoardModelTailReinforcements)
                .Include(c => c.BoardModelTails)
                .Single(b => b.Id == id);
        }

        public void Update(BoardModel entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<BoardModel> Where(Expression<Func<BoardModel, bool>> expression)
        {
            return _context.BoardModel.Include(c => c.BoardModelDimensions).Where(expression).AsQueryable();
        }

        public void Insert(BoardModel entity)
        {
            _context.BoardModel.Add(entity);
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
