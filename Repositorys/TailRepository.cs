using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class TailRepository<T> : ITailRepository<Tail> where T : BaseEntity
    {
        private DbSet<Tail> entities;
        private DbSet<IdentityUser> users;
        private readonly ApplicationDbContext _context;

        public TailRepository(ApplicationDbContext context)
        {
            entities = context.Set<Tail>();
            users = context.Set<IdentityUser>();
            _context = context;
        }

        public Tail Get(int id)
        {
            return entities.Select(x => new Tail
            {
                Id = x.Id,
                Name = x.Name,
                ImageName = x.ImageName,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                ApplicationUserId = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).Id,
                UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var entity = _context.Tail.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Active(int id)
        {
            var entity = _context.Tail.Single(x => x.Id == id);
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
