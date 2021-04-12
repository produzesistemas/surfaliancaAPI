using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShaperController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<Shaper> genericRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ShaperController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Shaper> genericRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.userManager = userManager;
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
            Expression<Func<Shaper, bool>> p2;
            var predicate = PredicateBuilder.New<Shaper>();
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
        public IActionResult Save(Shaper entity)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;
                var files = Request.Form.Files;
                if (entity.Id > decimal.Zero)
                {
                    var shaperBase = genericRepository.Get(entity.Id);
                    shaperBase.Name = entity.Name;
                    var extension = Path.GetExtension(files[0].FileName);
                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        files[0].CopyTo(stream);
                    }
                    fileDelete = string.Concat(fileDelete, shaperBase.ImageName);
                    shaperBase.ImageName = fileName;
                    genericRepository.Update(shaperBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }
                }
                else
                {
                    var extension = Path.GetExtension(files[0].FileName);
                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                         files[0].CopyTo(stream);
                     }
                    entity.ImageName = fileName;
                    genericRepository.Insert(entity);
                }
                return new JsonResult(entity);
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
    }
}
