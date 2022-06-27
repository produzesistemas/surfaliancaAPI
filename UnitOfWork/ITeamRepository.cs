using Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork
{
    public interface ITeamRepository : IDisposable
    {
        IQueryable<Team> GetAll();
        Team Get(int id);
        void Delete(int id);
        void Update(Team entity);
        void Insert(Team entity);
    }
}
