using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class TeamRepository<T> : ITeamRepository<Team> where T : BaseEntity
    {
        private DbSet<Team> entities;
        private DbSet<IdentityUser> users;
        private DbSet<TeamImage> teamImages;

        public TeamRepository(ApplicationDbContext context)
        {
            entities = context.Set<Team>();
            users = context.Set<IdentityUser>();
            teamImages = context.Set<TeamImage>();
        }

        public Team Get(int id)
        {
            return entities.Select(x => new Team
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                teamImages = teamImages.Where(t => t.TeamId == x.Id).ToList()
            }).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Team> Where(Func<Team, bool> expression)
        {
            return entities.Select(x => new Team
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                teamImages = teamImages.Where(t => t.TeamId == x.Id).ToList()
            }).Where(expression).AsQueryable();
        }
    }
}
