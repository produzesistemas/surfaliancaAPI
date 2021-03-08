using Microsoft.EntityFrameworkCore;
using Models;
using UnitOfWork;
using System;
using System.Linq;

namespace Repositorys
{
    public class CityRepository<T> : ICityRepository<City> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<City> entities;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<City>();
        }
        public City Get(int id)
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }

        public City GetByName(string name)
        {
            return entities.FirstOrDefault(x => x.Name.Equals(name));
        }

        public IQueryable<City> Where(Func<City, bool> expression)
        {
            return entities.Where(expression).AsQueryable();
        }
    }
}
