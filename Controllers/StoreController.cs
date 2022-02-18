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
        public StoreController(
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Store> genericRepository,
            IStoreRepository<Store> storeRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.storeRepository = storeRepository;

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
                var fileDeleteStore = pathToSave;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (store != null))
                {
                    if (store.Id == decimal.Zero)
                    {
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            foreach (var file in Request.Form.Files)
                            {
                                if (file.Name == "fileLogo")
                                {
                                    var extension = Path.GetExtension(file.FileName);
                                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                                    var fullPath = Path.Combine(pathToSave, fileName);
                                    using (var stream = new FileStream(fullPath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    store.ImageName = fileName;
                                }

                                if (file.Name == "fileStore")
                                {
                                    var extension = Path.GetExtension(file.FileName);
                                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                                    var fullPath = Path.Combine(pathToSave, fileName);
                                    using (var stream = new FileStream(fullPath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    store.ImageStore = fileName;
                                }

                            }
                            store.ApplicationUserId = id;
                            store.CreateDate = DateTime.Now;
                            genericRepository.Insert(store);
                            return new OkResult();
                        }
                    }
                    else
                    {
                        var lojaBase = genericRepository.Get(store.Id);
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            foreach (var file in Request.Form.Files)
                            {
                                if (file.Name == "fileLogo")
                                {
                                    var ext = Path.GetExtension(file.FileName);
                                    var fileNameLogo = string.Concat(Guid.NewGuid().ToString(), ext);
                                    var fullPath = Path.Combine(pathToSave, fileNameLogo);
                                    using (var stream = new FileStream(fullPath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    fileDelete = string.Concat(fileDelete, lojaBase.ImageName);
                                    store.ImageName = fileNameLogo;
                                }

                                if (file.Name == "fileStore")
                                {
                                    var ext = Path.GetExtension(file.FileName);
                                    var fileNameStore = string.Concat(Guid.NewGuid().ToString(), ext);
                                    var fullPath = Path.Combine(pathToSave, fileNameStore);
                                    using (var stream = new FileStream(fullPath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                    }
                                    fileDeleteStore = string.Concat(fileDeleteStore, lojaBase.ImageName);
                                    store.ImageStore = fileNameStore;
                                }

                            }

                        }
                        lojaBase.Description = store.Description;
                        lojaBase.Name = store.Name;
                        lojaBase.District = store.District;
                        lojaBase.Cep = store.Cep;
                        lojaBase.City = store.City;
                        lojaBase.CNPJ = store.CNPJ;
                        lojaBase.Contact = store.Contact;
                        lojaBase.Number = store.Number;
                        lojaBase.ValueMinimum = store.ValueMinimum;
                        if (store.NumberInstallmentsCard.HasValue)
                        {
                            lojaBase.NumberInstallmentsCard = store.NumberInstallmentsCard.Value;
                        }
                        lojaBase.ExchangePolicy = store.ExchangePolicy;
                        lojaBase.DeliveryPolicy = store.DeliveryPolicy;
                        lojaBase.UpdateApplicationUserId = id;
                        lojaBase.UpdateDate = DateTime.Now;
                        genericRepository.Update(lojaBase);
                        if (System.IO.File.Exists(fileDelete))
                        {
                            System.IO.File.Delete(fileDelete);
                        }
                        if (System.IO.File.Exists(fileDeleteStore))
                        {
                            System.IO.File.Delete(fileDeleteStore);
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

        [HttpGet()]
        [AllowAnonymous()]
        public IActionResult Get()
        {
            try
            {
                return new JsonResult(storeRepository.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
