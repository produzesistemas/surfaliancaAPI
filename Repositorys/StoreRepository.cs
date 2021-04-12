using Microsoft.AspNetCore.Identity;
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
        private DbSet<IdentityUser> users;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Store>();
            cidades = context.Set<City>();
            users = context.Set<IdentityUser>();
        }
        public IQueryable<Store> Where(Func<Store, bool> expression)
        {
            return entities.Select(x => new Store
            {
                Id = x.Id,
                CNPJ = x.CNPJ,
                Cep = x.Cep,
                CityId = x.CityId,
                Contact = x.Contact,
                DeliveryPolicy = x.DeliveryPolicy,
                Description = x.Description,
                ApplicationUserId = x.ApplicationUserId,
                UpdateApplicationUserId = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).Id,
                CriadoPor = users.FirstOrDefault(q => q.Id == x.ApplicationUserId).UserName,
                AlteradoPor = users.FirstOrDefault(q => q.Id == x.UpdateApplicationUserId).UserName,
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
            }).Where(expression).AsQueryable();
        }

    }
}
