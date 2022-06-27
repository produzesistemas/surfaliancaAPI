using Microsoft.AspNetCore.Mvc;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using UnitOfWork;
namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupomController : ControllerBase
    {
        private ICupomRepository cupomRepository;
        private readonly UserManager<IdentityUser> userManager;

        public CupomController(UserManager<IdentityUser> userManager,
            ICupomRepository cupomRepository)
        {
            this.cupomRepository = cupomRepository;
            this.userManager = userManager;
        }

        [HttpPost()]
        [Route("filter")]
        [Authorize()]
        public IActionResult GetByFilter(FilterDefault filter)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                Expression<Func<Coupon, bool>> p1;
                var predicate = PredicateBuilder.New<Coupon>();
                if (filter.Name != null)
                {
                    p1 = p => p.Code == filter.Name;
                    predicate = predicate.And(p1);
                    return new JsonResult(cupomRepository.Where(predicate).ToList());
                }

                return new JsonResult(cupomRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no carregamento dos Cupons: ", ex.Message));
            }
        }

        [HttpPost()]
        [Route("getByCodigo")]
        [Authorize()]
        public IActionResult GetByCodigo(FilterDefault filter)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                Expression<Func<Coupon, bool>> p1;
                var predicate = PredicateBuilder.New<Coupon>();
                p1 = p => p.Code == filter.Name;
                predicate = predicate.And(p1);
                var cupom = cupomRepository.Where(predicate).FirstOrDefault();
                if (cupom != null)
                {
                    if (!cupom.Active)
                    {
                        return BadRequest("Cupom expirado!");
                    }
                    if (!cupom.General)
                    {
                        if (cupom.ClientId != id)
                        {
                            return BadRequest("Esse cupom pertence a outro usuário!");
                        }
                    }
                    if ((DateTime.Now.Date >= cupom.InitialDate.Date) && (DateTime.Now.Date <= cupom.FinalDate.Date))
                    {
                        return new JsonResult(cupom);
                    }
                    else
                    {
                        return BadRequest("Cupom expirado!");
                    }

                    //Expression<Func<Order, bool>> p2, p3;
                    //var pred = PredicateBuilder.New<Order>();
                    //p2 = p => p.CouponId == cupom.Id;
                    //p3 = p => p.ApplicationUserId == id;
                    //pred = pred.And(p2);
                    //pred = pred.And(p3);
                    //if (cupomRepository.Where(pred).Any())
                    //{
                    //    return BadRequest("Cupom já utilizado!");
                    //}

                }

                return new JsonResult(cupom);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no carregamento dos Cupons: ", ex.Message));
            }
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(Coupon cupom)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                if (cupom.Id > decimal.Zero)
                {
                    cupomRepository.Update(cupom);
                }
                else
                {
                    cupomRepository.Insert(cupom);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(cupomRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Produto não encontrado!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                cupomRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Coupon cupom)
        {
            try
            {
                cupomRepository.Active(cupom.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }


    }
}
