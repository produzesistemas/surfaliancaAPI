using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class ShaperRepository<T> : IShaperRepository<Shaper> where T : BaseEntity
    {
        private DbSet<Shaper> entities;
        private DbSet<IdentityUser> users;

        public ShaperRepository(ApplicationDbContext context)
        {
            entities = context.Set<Shaper>();
            users = context.Set<IdentityUser>();
        }

        public Shaper Get(int id)
        {
            return entities.Select(x => new Shaper
            {
                Id = x.Id,
                Name = x.Name,
                ImageName = x.ImageName,
                Details = x.Details,
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
