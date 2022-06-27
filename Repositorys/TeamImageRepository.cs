using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class TeamImageRepository : ITeamImageRepository, IDisposable
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;
        public TeamImageRepository(ApplicationDbContext context)
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

        public void Delete(int id)
        {
            var entity = _context.TeamImage.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Insert(TeamImage entity)
        {
            _context.TeamImage.Add(entity);
            _context.SaveChanges();
        }
    }
}
