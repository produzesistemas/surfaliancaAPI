
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class LaminationRepository<T> : ILaminationRepository<Lamination> where T : BaseEntity
    {
        private DbSet<Lamination> entities;
        private DbSet<IdentityUser> users;

        public LaminationRepository(ApplicationDbContext context)
        {
            entities = context.Set<Lamination>();
            users = context.Set<IdentityUser>();
        }

        public Lamination Get(int id)
        {
            return entities.Select(x => new Lamination
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
    }
}
