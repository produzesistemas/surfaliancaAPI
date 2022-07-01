using System;
using Models;
using System.Linq;
using UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Repositorys
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;

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

        public Store Get()
        {
            return _context.Store.FirstOrDefault();
        }

        public void Update(Store entity)
        {
            var entityBase = _context.Store.Single(x => x.Id == entity.Id);
            entityBase.Name = entity.Name;
            entityBase.UpdateDate = DateTime.Now;
            entityBase.City = entity.City;
            entityBase.CNPJ = entity.CNPJ;
            entityBase.Contact = entity.Contact;
            entityBase.DeliveryPolicy = entity.DeliveryPolicy;
            entityBase.Description = entity.Description;
            entityBase.District = entity.District;
            entityBase.ExchangePolicy = entity.ExchangePolicy;
            if (entity.FreeShipping.HasValue) { entityBase.FreeShipping = entity.FreeShipping.Value; }
            if (entity.ValueMinimum.HasValue) { entityBase.ValueMinimum = entity.ValueMinimum.Value; }
            if (entity.OffPix.HasValue) { entityBase.OffPix = entity.OffPix.Value; }
            if (entity.NumberInstallmentsCard.HasValue) { entityBase.NumberInstallmentsCard = entity.NumberInstallmentsCard.Value; }
            entityBase.Number = entity.Number;
            entityBase.Phone = entity.Phone;
            entityBase.PostalCode = entity.PostalCode;
            entityBase.Street = entity.Street;
            entityBase.Warranty = entity.Warranty;
            entityBase.KeyPix = entity.KeyPix;

            _context.Entry(entityBase).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Insert(Store entity)
        {
            _context.Store.Add(entity);
            _context.SaveChanges();
        }
    }
}
