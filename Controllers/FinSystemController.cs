using System;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using LinqKit;
using System.Collections.Generic;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinSystemController : ControllerBase
    {
        private IRepository<FinSystem> genericRepository;
        private IFinSystemRepository<FinSystem> finSystemRepository;
        private IRepository<FinColor> finColorRepository;
        private IRepository<FinSystemColor> finSystemColorRepository;

        public FinSystemController(
            IRepository<FinSystem> genericRepository,
             IFinSystemRepository<FinSystem> finSystemRepository,
            IRepository<FinSystemColor> finSystemColorRepository,
            IRepository<FinColor> finColorRepository)
        {
            this.genericRepository = genericRepository;
            this.finSystemRepository = finSystemRepository;
            this.finColorRepository = finColorRepository;
            this.finSystemColorRepository = finSystemColorRepository;
        }

        [HttpPost()]
        [Route("filter")]
        [Authorize()]
        public IActionResult GetByFilter(FilterDefault filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
            if (id == null)
            {
                return BadRequest("Identificação do usuário não encontrada.");
            }
            Expression<Func<FinSystem, bool>> p2;
            var predicate = PredicateBuilder.New<FinSystem>();
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(genericRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(FinSystem finSystem)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                if (finSystem.Id > decimal.Zero)
                    {
                        var finSystemBase = finSystemRepository.Get(finSystem.Id);
                        finSystemBase.Name = finSystem.Name;
                        finSystemBase.UpdateApplicationUserId = id;
                        finSystemBase.UpdateDate = DateTime.Now;
                        genericRepository.Update(finSystemBase);
                        var toDelete = finSystemBase.FinSystemColors.Except(finSystem.FinSystemColors, new EqualityComparer()).ToList();
                        var toInsert = finSystem.FinSystemColors.Except(finSystemBase.FinSystemColors, new EqualityComparer()).ToList();
                        toDelete.ForEach(x =>
                        {
                            finSystemColorRepository.Delete(x);
                        });
                        toInsert.ForEach(x =>
                        {
                            x.FinSystemId = finSystemBase.Id;
                            finSystemColorRepository.Insert(x);
                        });
                }
                    else
                    {
                        finSystem.ApplicationUserId = id;
                        finSystem.CreateDate = DateTime.Now;
                        finSystem.Active = true;
                        genericRepository.Insert(finSystem);
                        finSystem.FinSystemColors.ForEach(finSystemColor =>
                        {
                            finSystemColor.FinSystemId = finSystem.Id;
                            finSystemColorRepository.Insert(finSystemColor);
                        });
                    }
                    return new JsonResult(finSystem);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                var entityBase = genericRepository.Get(id);
                genericRepository.Delete(entityBase);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(finSystemRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        class EqualityComparer : IEqualityComparer<FinSystemColor>
        {
            public bool Equals(FinSystemColor x, FinSystemColor y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.FinSystemId == y.FinSystemId && x.FinColorId == y.FinColorId && x.Id == y.Id;
            }

            public int GetHashCode(FinSystemColor obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
