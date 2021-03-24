using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<Store> genericRepository;
        private IStoreRepository<Store> storeRepository;
        private ICityRepository<City> cityRepository;
        private readonly UserManager<IdentityUser> userManager;
        public StoreController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Store> genericRepository,
            IStoreRepository<Store> storeRepository,
            ICityRepository<City> cityRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.storeRepository = storeRepository;
            this.cityRepository = cityRepository;
            this.userManager = userManager;
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var store = JsonConvert.DeserializeObject<Store>(Convert.ToString(Request.Form["store"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (store != null))
                {
                    if (store.Id == decimal.Zero)
                    {
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                            var fullPath = Path.Combine(pathToSave, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                Request.Form.Files[0].CopyTo(stream);
                            }
                            store.ImageName = fileName;
                            store.AspNetUsersId = id;
                            store.Active = true;
                            store.CityId = cityRepository.GetByName(store.NameCity).Id;
                            genericRepository.Insert(store);
                            return new OkResult();
                        }
                    } else {
                        var lojaBase = genericRepository.Get(store.Id);
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                            using (var stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
                            {
                                Request.Form.Files[0].CopyTo(stream);
                            }
                            fileDelete = string.Concat(fileDelete, lojaBase.ImageName);
                            lojaBase.ImageName = fileName;
                        }
                        lojaBase.Description = store.Description;
                        lojaBase.Name = store.Name;
                        lojaBase.District = store.District;
                        lojaBase.Cep = store.Cep;
                        lojaBase.CityId = cityRepository.GetByName(store.NameCity).Id;
                        lojaBase.CNPJ = store.CNPJ;
                        lojaBase.Contact = store.Contact;
                        lojaBase.Number = store.Number;
                        lojaBase.ExchangePolicy = store.ExchangePolicy;
                        lojaBase.DeliveryPolicy = store.DeliveryPolicy;
                        genericRepository.Update(lojaBase);
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
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost()]
        [Route("getByUser")]
        [Authorize()]
        public IActionResult GetByUser(ApplicationUser user)
        {
            try
            {
                return new JsonResult(storeRepository.GetByUser(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Falha no carregamento da Loja - " + ex.Message);
            }
        }
    }
}
