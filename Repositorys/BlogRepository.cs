using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork;


namespace Repositorys
{
    public class BlogRepository : IBlogRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Blog Get(int id)
        {
            return _context.Blog
                .Single(x => x.Id == id);
        }

        public IQueryable<Blog> Where(Expression<Func<Blog, bool>> expression)
        {
            return _context.Blog.Where(expression).Include(x => x.TypeBlog)
                 .AsQueryable();
        }

        public IQueryable<Blog> GetAll()
        {
            return _context.Blog.AsQueryable();
        }

        public void Delete(int id)
        {
            var entity = _context.Blog.Single(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Blog entity)
        {
            var entityBase = _context.Blog.Single(x => x.Id == entity.Id);

            entityBase.Description = entity.Description;
            entityBase.Details = entity.Details;
            entityBase.TypeBlogId = entity.TypeBlogId;
            entityBase.ImageName = entity.ImageName;
            entityBase.UpdateDate = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Blog entity)
        {
            _context.Blog.Add(entity);
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

