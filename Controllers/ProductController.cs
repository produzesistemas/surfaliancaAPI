using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        //private IRepository<Product> genericRepository;

        public ProductController(
           IWebHostEnvironment environment,
           IConfiguration Configuration
           //IRepository<Product> genericRepository
           )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            //this.genericRepository = genericRepository;
        }

        //[HttpPost()]
        //[Route("save")]
        //[Authorize()]
        //public IActionResult Save()
        //{
        //    try
        //    {
        //        var product = JsonConvert.DeserializeObject<Product>(Convert.ToString(Request.Form["product"]));
        //        var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileProduct"]);
        //        var fileDelete = pathToSave;
        //        ClaimsPrincipal currentUser = this.User;
        //        var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
        //        if ((id != null) || (product != null))
        //        {
        //            if (product.Id == decimal.Zero)
        //            {
        //                if (Request.Form.Files.Count() > decimal.Zero)
        //                {
        //                    var extension = Path.GetExtension(Request.Form.Files[0].FileName);
        //                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
        //                    var fullPath = Path.Combine(pathToSave, fileName);
        //                    using (var stream = new FileStream(fullPath, FileMode.Create))
        //                    {
        //                        Request.Form.Files[0].CopyTo(stream);
        //                    }
        //                    product.ImageName = fileName;
        //                    product.ApplicationUserId = id;
        //                    product.CreateDate = DateTime.Now;
        //                    genericRepository.Insert(product);
        //                    return new OkResult();
        //                }
        //            }
        //            else
        //            {
        //                var productBase = genericRepository.Get(product.Id);
        //                if (Request.Form.Files.Count() > decimal.Zero)
        //                {
        //                    var extension = Path.GetExtension(Request.Form.Files[0].FileName);
        //                    var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
        //                    using (var stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
        //                    {
        //                        Request.Form.Files[0].CopyTo(stream);
        //                    }
        //                    fileDelete = string.Concat(fileDelete, productBase.ImageName);
        //                    productBase.ImageName = fileName;
        //                }
        //                productBase.Description = product.Description;
        //                productBase.Value = product.Value;
        //                productBase.BoardModelId = product.BoardModelId;
        //                productBase.BoardTypeId = product.BoardTypeId;
        //                productBase.BottomId = product.BottomId;
        //                productBase.ConstructionId = product.ConstructionId;
        //                productBase.FinSystemId = product.FinSystemId;
        //                productBase.LaminationId = product.LaminationId;
        //                productBase.LitigationId = product.LitigationId;
        //                productBase.TailId = product.TailId;
        //                productBase.ProductStatusId = product.ProductStatusId;
        //                productBase.ProductTypeId = product.ProductTypeId;
        //                productBase.ShaperId = product.ShaperId;
        //                productBase.SizeId = product.SizeId;
        //                productBase.TypeSaleId = product.TypeSaleId;
        //                productBase.WidthId = product.WidthId;
        //                productBase.UpdateApplicationUserId = id;
        //                productBase.UpdateDate = DateTime.Now;
        //                genericRepository.Update(productBase);
        //                if (System.IO.File.Exists(fileDelete))
        //                {
        //                    System.IO.File.Delete(fileDelete);
        //                }
        //                return new OkResult();
        //            }

        //        }

        //        return new OkResult();

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }

        //}
    }
}
