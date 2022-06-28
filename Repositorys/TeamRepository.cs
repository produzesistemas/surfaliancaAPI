using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;

namespace Repositorys
{
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;
        public TeamRepository(ApplicationDbContext context)
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

        public Team Get(int id)
        {
            return _context.Team.Include(x => x.teamImages).Single(b => b.Id == id);
        }

        public void Delete(int id)
        {
            var entity = _context.Team.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Team entity)
        {
            var entityBase = _context.Team.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.Description = entity.Description;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Team entity)
        {
            _context.Team.Add(entity);
            _context.SaveChanges();
        }

        public IQueryable<Team> GetAll()
        {
            return _context.Team.Include(x => x.teamImages).AsQueryable();
        }

    }
}
