using Microsoft.EntityFrameworkCore;
using Models;
using UnitOfWork;
using System;
using System.Linq;

namespace Repositorys
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
    {
        _context = context;
            entities = context.Set<T>();
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
           return entities.AsQueryable();
        }
        

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<T> Where(Func<T, bool> expression)
        {
            return entities.Where(expression).AsQueryable();
        }
    }
}
