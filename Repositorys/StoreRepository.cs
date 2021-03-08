using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using UnitOfWork;

namespace Repositorys
{
    public class StoreRepository<T> : IStoreRepository<Store> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Store> entities;
        private readonly DbSet<City> cidades;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Store>();
            cidades = context.Set<City>();
        }
        public bool CheckExist(string id)
        {
            return entities.Where(x => x.AspNetUsersId == id).Any();
        }

        public Store GetByUser(string id)
        {
            return entities.Select(x => new Store
            {
                Id = x.Id,
                CNPJ = x.CNPJ,
                Cep = x.Cep,
                 Active = x.Active,
                  CityId = x.CityId,
                   Contact = x.Contact,
                    DeliveryPolicy = x.DeliveryPolicy,
                     Description = x.Description,
                AspNetUsersId = x.AspNetUsersId,
                 City = cidades.FirstOrDefault(c => c.Id == x.CityId),
                  Name = x.Name,
                   District = x.District,
                    ExchangePolicy = x.ExchangePolicy,
                     ImageName = x.ImageName,
                      Number = x.Number,
                       Phone = x.Phone,
                        Street = x.Street,
                         TaxDelivery = x.TaxDelivery,
                          ValueMinimum = x.ValueMinimum
            }).FirstOrDefault(x => x.AspNetUsersId == id);
        }

        public IQueryable<Store> Where(Func<Store, bool> expression)
        {
            return entities.Where(expression).AsQueryable();
        }

    }
}
