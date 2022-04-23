﻿using Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Repositorys
{
    public class BoardModelRepository<T> : IBoardModelRepository<BoardModel> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BoardModelRepository(ApplicationDbContext context)
        {
            _context = context;
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
            if ((_context.OrderProductOrdered.Any(c => c.BoardModelId == id)) ||
                (_context.Paint.Any(c => c.BoardModelId == id)))
            {
                throw new Exception("O modelo não pode ser excluído.Está relacionado com um pedido ou com uma pintura.Considere desativar!");
            };

            var dimensions = _context.BoardModelDimensions.Where(c => c.BoardModelId == id);
            _context.RemoveRange(dimensions);
            var paints = _context.Paint.Where(c => c.BoardModelId == id);
            _context.RemoveRange(paints);
            var entity = _context.BoardModel.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
            _context.Dispose();
        }

        public BoardModel Get(int id)
        {
                var b = _context.BoardModel.Single(b => b.Id == id);
                _context.Entry(b).Collection(b => b.BoardModelDimensions).Query().Load();
                return b;
        }

        public void Update(BoardModel entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<BoardModel> Where(Func<BoardModel, bool> expression)
        {
            return _context.BoardModel.Include(c => c.BoardModelDimensions).Where(expression).AsQueryable();
        }
    }
}
