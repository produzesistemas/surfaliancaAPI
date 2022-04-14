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
        private IRepository<Stringer> genericRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public StringerController(
            IRepository<Stringer> genericRepository,
                            IWebHostEnvironment environment,
            IConfiguration Configuration)
        {
            this.genericRepository = genericRepository;
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
            return new JsonResult(genericRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var stringer = JsonConvert.DeserializeObject<Stringer>(Convert.ToString(Request.Form["stringer"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;

                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                var files = Request.Form.Files;

                if (stringer.Id > decimal.Zero)
                {
                    var entityBase = genericRepository.Get(stringer.Id);

                    for (var counter = 0; counter < files.Count; counter++)
                    {
                        var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[counter].CopyTo(stream);
                        }

                        entityBase.ImageName = fileName;
                    };


                    entityBase.Name = stringer.Name;
                    if (stringer.Value.HasValue)
                    {
                        entityBase.Value = stringer.Value;
                    }
                    entityBase.Details = stringer.Details;
                    if (stringer.Value.HasValue)
                    {
                        entityBase.Value = stringer.Value.Value;
                    } else
                    {
                        entityBase.Value = null;
                    }

                    entityBase.UpdateApplicationUserId = id;
                    entityBase.UpdateDate = DateTime.Now;
                    genericRepository.Update(entityBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }
                }
                else
                {
                    for (var counter = 0; counter < files.Count; counter++)
                    {
                        var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[counter].CopyTo(stream);
                        }
                        stringer.ImageName = fileName;
                    };

                    stringer.ApplicationUserId = id;
                    stringer.CreateDate = DateTime.Now;
                    stringer.Active = true;
                    genericRepository.Insert(stringer);
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
                return new JsonResult(genericRepository.Get(id));
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
            return new JsonResult(genericRepository.GetAll().ToList());
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Stringer stringer)
        {
            try
            {
                var entity = genericRepository.Get(stringer.Id);
                if (entity.Active)
                {
                    entity.Active = false;
                }
                else
                {
                    entity.Active = true;
                }
                genericRepository.Update(entity);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}

