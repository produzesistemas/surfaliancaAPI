using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class StringerRepository<T> : IStringerRepository<Stringer> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        public StringerRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public void Delete(int id)
        {
            var entity = _context.Stringer.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Active(int id)
        {
            var entity = _context.Stringer.Single(x => x.Id == id);
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
