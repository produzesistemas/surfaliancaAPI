using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class LogoRepository<T> : ILogoRepository<Logo> where T : BaseEntity
    {
        private DbSet<Logo> entities;
        private DbSet<IdentityUser> users;

        public LogoRepository(ApplicationDbContext context)
        {
            entities = context.Set<Logo>();
            users = context.Set<IdentityUser>();
        }

        public Logo Get(int id)
        {
            return entities.Select(x => new Logo
            {
                Id = x.Id,
                Name = x.Name,
                ImageName = x.ImageName,
                Value = x.Value,
                Active = x.Active,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                ApplicationUserId = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).Id,
                UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
