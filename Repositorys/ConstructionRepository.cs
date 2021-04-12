using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;


namespace Repositorys
{
    public class ConstructionRepository<T> : IConstructionRepository<Construction> where T : BaseEntity
    {
        private DbSet<Construction> entities;
        private DbSet<IdentityUser> users;

        public ConstructionRepository(ApplicationDbContext context)
        {
            entities = context.Set<Construction>();
            users = context.Set<IdentityUser>();
        }

        public Construction Get(int id)
        {
            return entities.Select(x => new Construction
            {
                Id = x.Id,
                Name = x.Name,
                Details = x.Details,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                ApplicationUserId = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).Id,
                UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Construction> Where(Func<Construction, bool> expression)
        {
            return entities.Select(x => new Construction
            {
                Id = x.Id,
                Name = x.Name,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).Where(expression).AsQueryable();
        }
    }
}
