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
    public class TailReinforcementController : ControllerBase
    {
        private ITailReinforcementRepository tailReinforcementRepository;

        public TailReinforcementController(
             ITailReinforcementRepository tailReinforcementRepository)
        {
            this.tailReinforcementRepository = tailReinforcementRepository;
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
            Expression<Func<TailReinforcement, bool>> p2;
            var predicate = PredicateBuilder.New<TailReinforcement>();
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
                return new JsonResult(tailReinforcementRepository.Where(predicate));
            }
            return new JsonResult(tailReinforcementRepository.GetAll().OrderBy(x => x.Name));
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(TailReinforcement tailReinforcement)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                if (tailReinforcement.Id > decimal.Zero)
                {
                    tailReinforcementRepository.Update(tailReinforcement);
                }
                else
                {
                    tailReinforcement.Active = true;
                    tailReinforcement.ApplicationUserId = id;
                    tailReinforcement.CreateDate = DateTime.Now;
                    tailReinforcementRepository.Insert(tailReinforcement);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no cadastro da construção: ", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                tailReinforcementRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return BadRequest("O reforço da rabeta não pode ser excluído. Está relacionado com um pedido. Considere desativar!");
                }
                return BadRequest(string.Concat("Falha na exclusão do reforço: ", ex.Message));
            }


        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(tailReinforcementRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        [HttpGet()]
        [Route("getAll")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return new JsonResult(tailReinforcementRepository.GetAll().ToList());
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(TailReinforcement tailReinforcement)
        {
            try
            {
                tailReinforcementRepository.Active(tailReinforcement.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na ativação do reforço da rabeta: ", ex.Message));
            }
        }

    }
}
