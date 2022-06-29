using System;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using LinqKit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        private IConstructionRepository constructionRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public ConstructionController(
                            IWebHostEnvironment environment,
            IConfiguration Configuration,
             IConstructionRepository constructionRepository)
        {
            this.constructionRepository = constructionRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
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
            Expression<Func<Construction, bool>> p2;
            var predicate = PredicateBuilder.New<Construction>();
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(constructionRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(Construction construction)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                if (construction.Id > decimal.Zero)
                {
                    constructionRepository.Update(construction);
                }
                else
                {
                    construction.Active = true;
                    construction.ApplicationUserId = id;
                    construction.CreateDate = DateTime.Now;
                    constructionRepository.Insert(construction);
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
                constructionRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return BadRequest("A tecnologia / construção não pode ser excluída. Está relacionada com um pedido. Considere desativar!");
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
                return new JsonResult(constructionRepository.Get(id));
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
            return new JsonResult(constructionRepository.GetAll().ToList());
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Construction construction)
        {
            try
            {
                constructionRepository.Active(construction.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}
