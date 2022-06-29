using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IO;

namespace petixcoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingCompanyController : ControllerBase
    {
        private IShippingCompanyRepository shippingCompanyRepository;
        private IShippingCompanyStateRepository shippingCompanyStateRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public ShippingCompanyController(
                            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IShippingCompanyRepository shippingCompanyRepository, IShippingCompanyStateRepository shippingCompanyStateRepository)
        {
            this.shippingCompanyRepository = shippingCompanyRepository;
            this.shippingCompanyStateRepository = shippingCompanyStateRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
        }

        [HttpPost()]
        [Route("filter")]
        [Authorize()]
        public IActionResult GetByFilter(FilterDefault filter)
        {
            try
            {
                Expression<Func<ShippingCompany, bool>> p1;
                var predicate = PredicateBuilder.New<ShippingCompany>();
                if (filter.Name != "")
                {
                    p1 = p => p.Name == filter.Name;
                    predicate = predicate.And(p1);
                    return new JsonResult(shippingCompanyRepository.Where(predicate).OrderBy(x => x.Name).ToList());
                }

                return new JsonResult(shippingCompanyRepository.GetAll().OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no carregamento dos bairros: ", ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(shippingCompanyRepository.Get(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var shippingCompany = JsonConvert.DeserializeObject<ShippingCompany>(Convert.ToString(Request.Form["shippingCompany"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;

                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                var files = Request.Form.Files;
                if (shippingCompany.Id > decimal.Zero)
                {
                    var shippingCompanyBase = shippingCompanyRepository.Get(shippingCompany.Id);
                    for (var counter = 0; counter < files.Count; counter++)
                    {
                        var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[counter].CopyTo(stream);
                        }

                        switch (counter)
                        {
                            case 0:
                                shippingCompany.ImageName = fileName;
                                break;
                        }

                    };

                    shippingCompany.Name = shippingCompany.Name;
                    shippingCompany.UpdateApplicationUserId = id;
                    shippingCompany.UpdateDate = DateTime.Now;
                    shippingCompanyRepository.Update(shippingCompany);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }

                    var toDelete = shippingCompanyBase.ShippingCompanyStates.Except(shippingCompany.ShippingCompanyStates, new EqualityComparer()).ToList();
                    var toInsert = shippingCompany.ShippingCompanyStates.Except(shippingCompanyBase.ShippingCompanyStates, new EqualityComparer()).ToList();
                    toDelete.ForEach(x =>
                    {
                        x.ShippingCompany = null;
                        shippingCompanyStateRepository.Delete(x.Id);
                    });
                    toInsert.ForEach(x =>
                    {
                        x.ShippingCompanyId = shippingCompany.Id;
                        x.Id = 0;
                        shippingCompanyStateRepository.Insert(x);

                    });

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

                        switch (counter)
                        {
                            case 0:
                                shippingCompany.ImageName = fileName;
                                break;
                        }

                    };

                    shippingCompany.ApplicationUserId = id;
                    shippingCompany.CreateDate = DateTime.Now;
                    shippingCompany.Active = true;
                    shippingCompanyRepository.Insert(shippingCompany);

                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na tentativa: ", ex.Message));
            }
        }


        class EqualityComparer : IEqualityComparer<ShippingCompanyState>
        {
            public bool Equals(ShippingCompanyState x, ShippingCompanyState y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.StateId == y.StateId && x.TaxValue == y.TaxValue;
            }

            public int GetHashCode(ShippingCompanyState obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                shippingCompanyRepository.Delete(id);
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
        public IActionResult Active(ShippingCompany shippingCompany)
        {
            try
            {
                shippingCompanyRepository.Active(shippingCompany.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

    }
}


