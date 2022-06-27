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
    public class StringerController : ControllerBase
    {
        private IStringerRepository stringerRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public StringerController(
            IStringerRepository stringerRepository,
                            IWebHostEnvironment environment,
            IConfiguration Configuration)
        {
            this.stringerRepository = stringerRepository;
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
            Expression<Func<Stringer, bool>> p2;
            var predicate = PredicateBuilder.New<Stringer>();
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(stringerRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(Stringer stringer)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                if (stringer.Id > decimal.Zero)
                {
                    stringerRepository.Update(stringer);
                }
                else
                {
                    stringer.ApplicationUserId = id;
                    stringer.CreateDate = DateTime.Now;
                    stringer.Active = true;
                    stringerRepository.Insert(stringer);
                }
                return new OkResult();
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
               stringerRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return BadRequest("A longarina não pode ser excluída. Está relacionada com um pedido. Considere desativar!");
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
                return new JsonResult(stringerRepository.Get(id));
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
            return new JsonResult(stringerRepository.GetAll().ToList());
        }


        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Stringer stringer)
        {
            try
            {
                stringerRepository.Active(stringer.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}

