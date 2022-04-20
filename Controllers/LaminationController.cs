using System;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using LinqKit;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaminationController : ControllerBase
    {
        private IRepository<Lamination> genericRepository;
        private ILaminationRepository<Lamination> laminationRepository;

        public LaminationController(
            IRepository<Lamination> genericRepository,
             ILaminationRepository<Lamination> laminationRepository)
        {
            this.genericRepository = genericRepository;
            this.laminationRepository = laminationRepository;
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
            Expression<Func<Lamination, bool>> p2;
            var predicate = PredicateBuilder.New<Lamination>();
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
        public IActionResult Save(Lamination entity)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                if (entity.Id > decimal.Zero)
                {
                    var entityBase = genericRepository.Get(entity.Id);
                    entityBase.Name = entity.Name;
                    entityBase.Details = entity.Details;
                    entityBase.UpdateApplicationUserId = id;
                    entityBase.UpdateDate = DateTime.Now;
                    genericRepository.Update(entityBase);
                }
                else
                {
                    entity.ApplicationUserId = id;
                    entity.CreateDate = DateTime.Now;
                    genericRepository.Insert(entity);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na tentativa: ", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                laminationRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return BadRequest("A laminação não pode ser excluída. Está relacionada com um pedido. Considere desativar!");
                }
                return BadRequest(string.Concat("Falha na exclusão da construção: ", ex.Message));
            }


        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(laminationRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        [HttpGet()]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(genericRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Lamination lamination)
        {
            try
            {
                laminationRepository.Active(lamination.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}
