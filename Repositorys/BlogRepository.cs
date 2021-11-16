using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;


namespace Repositorys
{
    public class BlogRepository<T> : IBlogRepository<Blog> where T : BaseEntity
    {
        private DbSet<Blog> entities;
        private DbSet<IdentityUser> users;

        public BlogRepository(ApplicationDbContext context)
        {
            entities = context.Set<Blog>();
            users = context.Set<IdentityUser>();
        }

        public Blog Get(int id)
        {
            return entities.Select(x => new Blog
            {
                Id = x.Id,
                Description = x.Description,
                TypeBlogId = x.TypeBlogId,
                Details = x.Details,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate,
                ApplicationUserId = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).Id,
                UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Blog> Where(Func<Blog, bool> expression)
        {
            return entities.Select(x => new Blog
            {
                Id = x.Id,
                Description = x.Description,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
            }).Where(expression).AsQueryable();
        }
    }
}

