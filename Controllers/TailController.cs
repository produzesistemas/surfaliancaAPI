using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<Tail> genericRepository;
        private ITailRepository<Tail> tailRepository;

        public TailController(
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Tail> genericRepository, ITailRepository<Tail> tailRepository
            )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.tailRepository = tailRepository;
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
            Expression<Func<Tail, bool>> p2;
            var predicate = PredicateBuilder.New<Tail>();
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
                var tail = JsonConvert.DeserializeObject<Tail>(Convert.ToString(Request.Form["tail"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                var files = Request.Form.Files;
                if (tail.Id > decimal.Zero)
                {
                    var tailBase = genericRepository.Get(tail.Id);
                    tailBase.Name = tail.Name;
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[0].CopyTo(stream);
                        }
                        fileDelete = string.Concat(fileDelete, tailBase.ImageName);
                        tailBase.ImageName = fileName;
                    }
                    tailBase.UpdateApplicationUserId = id;
                    tailBase.UpdateDate = DateTime.Now;
                    genericRepository.Update(tailBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }
                }
                else
                {
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[0].CopyTo(stream);
                        }
                        tail.ImageName = fileName;
                        tail.ApplicationUserId = id;
                        tail.CreateDate = DateTime.Now;
                        genericRepository.Insert(tail);
                    }
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
                return new JsonResult(tailRepository.Get(id));
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
    }
}
