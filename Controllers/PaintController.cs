using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
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
    public class PaintController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<Paint> genericRepository;
        private IPaintRepository<Paint> paintRepository;
        private readonly UserManager<IdentityUser> userManager;

        public PaintController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Paint> genericRepository,
            IPaintRepository<Paint> paintRepository
            )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.paintRepository = paintRepository;
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
            Expression<Func<Paint, bool>> p2;
            var predicate = PredicateBuilder.New<Paint>();
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
                var paint = JsonConvert.DeserializeObject<Paint>(Convert.ToString(Request.Form["paint"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;

                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                var files = Request.Form.Files;
                if (paint.Id > decimal.Zero)
                {
                    var paintBase = genericRepository.Get(paint.Id);
                    paintBase.Name = paint.Name;
                    paintBase.BoardModelId = paint.BoardModelId;
                    if (paintBase.Value.HasValue)
                    {
                        paintBase.Value = paint.Value;
                    }
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[0].CopyTo(stream);
                        }
                        fileDelete = string.Concat(fileDelete, paint.ImageName);
                        paintBase.ImageName = fileName;
                    }
                    paintBase.UpdateApplicationUserId = id;
                    paintBase.UpdateDate = DateTime.Now;

                    genericRepository.Update(paintBase);
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
                        paint.ImageName = fileName;
                        paint.ApplicationUserId = id;
                        paint.CreateDate = DateTime.Now;
                        paint.Active = true;
                        genericRepository.Insert(paint);
                    }
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
                return new JsonResult(paintRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        [HttpPost()]
        [Route("getAll")]
        [AllowAnonymous]
        public IActionResult GetAll(FilterDefault filter)
        {
            Expression<Func<Paint, bool>> p2;
            var predicate = PredicateBuilder.New<Paint>();
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(genericRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(Paint paint)
        {
            try
            {
                var paintBase = genericRepository.Get(paint.Id);
                if (paintBase.Active)
                {
                    paintBase.Active = false;
                }
                else
                {
                    paintBase.Active = true;
                }
                genericRepository.Update(paintBase);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPost()]
        [Route("getByModel")]
        [AllowAnonymous]
        public IActionResult GetByModel(FilterDefault filter)
        {
            Expression<Func<Paint, bool>> p2;
            var predicate = PredicateBuilder.New<Paint>();
            if (filter.Id > 0)
            {
                p2 = p => p.BoardModelId == filter.Id;
                predicate = predicate.And(p2);
            }
            return new JsonResult(genericRepository.Where(predicate).ToList());
        }
    }
}


