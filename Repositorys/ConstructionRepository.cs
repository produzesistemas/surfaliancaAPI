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
        private readonly ApplicationDbContext _context;
        public ConstructionRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Construction>();
            users = context.Set<IdentityUser>();
        }

        public Construction Get(int id)
        {
            return entities.Select(x => new Construction
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                Details = x.Details,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                UrlMovie = x.UrlMovie,
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

        public void Delete(int id)
        {
            var entity = _context.Construction.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Active(int id)
        {
            var entity = _context.Construction.Single(x => x.Id == id);
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
