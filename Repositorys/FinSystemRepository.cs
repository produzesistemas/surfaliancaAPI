using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class FinSystemRepository<T> : IFinSystemRepository<FinSystem> where T : BaseEntity
    {
        private DbSet<FinSystem> entities;
        private DbSet<IdentityUser> users;
        private DbSet<FinSystemColor> finSystemColors;
        private DbSet<FinColor> finColors;

        public FinSystemRepository(ApplicationDbContext context)
        {
            entities = context.Set<FinSystem>();
            users = context.Set<IdentityUser>();
            finSystemColors = context.Set<FinSystemColor>();
            finColors = context.Set<FinColor>();
        }

        public FinSystem Get(int id)
        {
            return entities.Select(x => new FinSystem
            {
                Id = x.Id,
                Name = x.Name,
                FinSystemColors = finSystemColors.Where(t => t.FinSystemId == x.Id).ToList(),
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<FinSystem> Where(Func<FinSystem, bool> expression)
        {
            return entities.Select(x => new FinSystem
            {
                Id = x.Id,
                Name = x.Name,
                FinSystemColors = finSystemColors.Where(t => t.FinSystemId == x.Id).ToList(),
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).Where(expression).AsQueryable();
        }
    }
}
