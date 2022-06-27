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
        private ICupomRepository<Coupon> cupomRepository;
        private IRepository<Coupon> genericRepository;
        private IRepository<Order> pedidoRepository;
        private readonly UserManager<IdentityUser> userManager;

        public CupomController(UserManager<IdentityUser> userManager,
            IRepository<Coupon> genericRepository,
            IRepository<Order> pedidoRepository,
            ICupomRepository<Coupon> cupomRepository)
        {
            this.cupomRepository = cupomRepository;
            this.userManager = userManager;
            this.genericRepository = genericRepository;
            this.pedidoRepository = pedidoRepository;
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
                    p1 = p => p.Codigo == filter.Name;
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
                p1 = p => p.Codigo == filter.Name;
                predicate = predicate.And(p1);
                var cupom = cupomRepository.Where(predicate).FirstOrDefault();
                if (cupom != null)
                {
                    if (!cupom.Ativo)
                    {
                        return BadRequest("Cupom expirado!");
                    }
                    if (!cupom.Geral)
                    {
                        if (cupom.ClienteId != id)
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

                    Expression<Func<Order, bool>> p2, p3;
                    var pred = PredicateBuilder.New<Order>();
                    p2 = p => p.CupomId == cupom.Id;
                    p3 = p => p.ApplicationUserId == id;
                    pred = pred.And(p2);
                    pred = pred.And(p3);
                    if (pedidoRepository.Where(pred).Any())
                    {
                        return BadRequest("Cupom já utilizado!");
                    }

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
                    var cupomBase = genericRepository.Get(cupom.Id);
                    cupomBase.Descricao = cupom.Descricao;
                    cupomBase.UpdateApplicationUserId = id;
                    cupomBase.UpdateDate = DateTime.Now;
                    cupomBase.Codigo = cupom.Codigo;
                    cupomBase.InitialDate = cupom.InitialDate;
                    cupomBase.FinalDate = cupom.FinalDate;
                    cupomBase.Geral = cupom.Geral;
                    cupomBase.Quantidade = cupom.Quantidade;
                    cupomBase.Tipo = cupom.Tipo;
                    cupomBase.Valor = cupom.Valor;
                    cupomBase.ValorMinimo = cupom.ValorMinimo;
                    if (cupom.ClienteId != null)
                    {
                        cupomBase.ClienteId = cupom.ClienteId;
                    }

                    genericRepository.Update(cupomBase);
                }
                else
                {
                    cupom.Ativo = true;
                    cupom.AspNetUsersId = id;
                    cupom.CreateDate = DateTime.Now;
                    if (cupom.ClienteId == "") cupom.ClienteId = null;
                    genericRepository.Insert(cupom);
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
                //if (pedidoRepository.Where(x => x.CupomId == id).Any())
                //{
                //    return BadRequest("Sem permissão para excluir. Cupom já foi aplicado!");
                //}
                var entityBase = cupomRepository.Get(id);
                genericRepository.Delete(entityBase);
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
                var cupomBase = cupomRepository.Get(cupom.Id);
                if (cupomBase.Ativo)
                {
                    cupomBase.Ativo = false;
                }
                else
                {
                    cupomBase.Ativo = true;
                }
                genericRepository.Update(cupomBase);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }


    }
}
