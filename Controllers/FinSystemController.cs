using System;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using LinqKit;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinSystemController : ControllerBase
    {
        private IRepository<FinSystem> genericRepository;
        private IFinSystemRepository<FinSystem> finSystemRepository;
        private IRepository<FinColor> finColorRepository;
        private IRepository<FinSystemColor> finSystemColorRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        public FinSystemController(
                       IWebHostEnvironment environment,
           IConfiguration Configuration,
            IRepository<FinSystem> genericRepository,
             IFinSystemRepository<FinSystem> finSystemRepository,
            IRepository<FinSystemColor> finSystemColorRepository,
            IRepository<FinColor> finColorRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.finSystemRepository = finSystemRepository;
            this.finColorRepository = finColorRepository;
            this.finSystemColorRepository = finSystemColorRepository;
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
            Expression<Func<FinSystem, bool>> p2;
            var predicate = PredicateBuilder.New<FinSystem>();
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
                var finSystem = JsonConvert.DeserializeObject<FinSystem>(Convert.ToString(Request.Form["finSystem"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileProduct"]);
                var fileDelete = pathToSave;
                var files = Request.Form.Files;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (finSystem != null))
                {
                    if (finSystem.Id == decimal.Zero)
                    {
                        if (Request.Form.Files.Count() > decimal.Zero)
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

                                finSystem.ImageName = fileName;

                            };

                            finSystem.ApplicationUserId = id;
                            finSystem.CreateDate = DateTime.Now;
                            finSystem.Active = true;
                            genericRepository.Insert(finSystem);
                            return new OkResult();
                        }
                    }
                    else
                    {
                        var finSystemBase = genericRepository.Get(finSystem.Id);
                        if (Request.Form.Files.Count() > decimal.Zero)
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
                                finSystemBase.ImageName = fileName;
                            };
                        }
                        finSystemBase.Details = finSystem.Details;
                        finSystemBase.Name = finSystem.Name;
                        finSystemBase.UpdateApplicationUserId = id;
                        finSystemBase.UpdateDate = DateTime.Now;
                        genericRepository.Update(finSystemBase);
                        if (System.IO.File.Exists(fileDelete))
                        {
                            System.IO.File.Delete(fileDelete);
                        }
                        return new OkResult();
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
                return new JsonResult(finSystemRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        class EqualityComparer : IEqualityComparer<FinSystemColor>
        {
            public bool Equals(FinSystemColor x, FinSystemColor y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.FinSystemId == y.FinSystemId && x.FinColorId == y.FinColorId && x.Id == y.Id;
            }

            public int GetHashCode(FinSystemColor obj)
            {
                return obj.Id.GetHashCode();
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
