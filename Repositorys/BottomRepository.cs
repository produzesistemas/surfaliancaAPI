using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class BottomRepository<T> : IBottomRepository<Bottom> where T : BaseEntity
    {
        private DbSet<Bottom> entities;
        private DbSet<IdentityUser> users;

        public BottomRepository(ApplicationDbContext context)
        {
            entities = context.Set<Bottom>();
            users = context.Set<IdentityUser>();
        }

        public Bottom Get(int id)
        {
            return entities.Select(x => new Bottom
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
    }
}
