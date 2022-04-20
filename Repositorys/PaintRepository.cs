﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class PaintRepository<T> : IPaintRepository<Paint> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public PaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Paint Get(int id)
        {
            return _context.Paint.Single(b => b.Id == id);
        }

        public void Delete(int id)
        {
            var entity = _context.Paint.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Active(int id)
        {
            var entity = _context.Paint.Single(x => x.Id == id);
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
    }
}
