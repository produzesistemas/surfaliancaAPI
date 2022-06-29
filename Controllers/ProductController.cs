using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ProductController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IProductRepository productRepository;
        private IProductTypeRepository productTypeRepository;
        private IProductStatusRepository productStatusRepository;
        //private IRepository<TypeSale> typeSaleRepository;
        //private IRepository<ProductStatus> productStatusRepository;
        //private IRepository<ProductType> productTypeRepository;
        //private IProductRepository<Product> productRepository;

        public ProductController(
           IWebHostEnvironment environment,
           IConfiguration Configuration,
           IProductRepository productRepository,
           IProductTypeRepository productTypeRepository,
           IProductStatusRepository productStatusRepository
        //   IProductRepository<Product> productRepository,
        //   IRepository<TypeSale> typeSaleRepository,
        //IRepository<ProductStatus> productStatusRepository,
        //IRepository<ProductType> productTypeRepository
           )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.productRepository = productRepository;
            this.productStatusRepository = productStatusRepository;
            this.productTypeRepository = productTypeRepository;
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
            Expression<Func<Product, bool>> p2, p1;
            var predicate = PredicateBuilder.New<Product>();
            p1 = p => p.Active.Equals(true);
            predicate = predicate.And(p1);
            if (filter.Name != null)
            {
                p2 = p => p.Name.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(productRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("getByType")]
        public IActionResult GetByType(FilterDefault filter)
        {
            Expression<Func<Product, bool>> p2, p1;
            var predicate = PredicateBuilder.New<Product>();
            p1 = p => p.Active.Equals(true);
            predicate = predicate.And(p1);
            if (filter.Id > 0)
            {
                p2 = p => p.ProductTypeId == filter.Id;
                predicate = predicate.And(p2);
            }
            return new JsonResult(productRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(Convert.ToString(Request.Form["product"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileProduct"]);
                var fileDelete = pathToSave;
                var files = Request.Form.Files;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (product != null))
                {
                    if (product.Id == decimal.Zero)
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

                                switch (counter)
                                {
                                    case 0:
                                        product.ImageName = fileName;
                                        break;
                                    case 1:
                                        product.ImageName1 = fileName;
                                        break;
                                    case 2:
                                        product.ImageName2 = fileName;
                                        break;
                                }
                
                            };
                            
                            product.ApplicationUserId = id;
                            product.CreateDate = DateTime.Now;
                            product.Active = true;
                            productRepository.Insert(product);
                        }
                    }
                    else
                    {
                        var productBase = productRepository.Get(product.Id);
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

                                switch (counter)
                                {
                                    case 0:
                                        product.ImageName = fileName;
                                        break;
                                    case 1:
                                        product.ImageName1 = fileName;
                                        break;
                                    case 2:
                                        product.ImageName2 = fileName;
                                        break;
                                }

                            };
                            fileDelete = string.Concat(fileDelete, productBase.ImageName);
                        }
                        else
                        {
                            product.ImageName = productBase.ImageName;
                            product.ImageName1 = productBase.ImageName1;
                            product.ImageName2 = productBase.ImageName2;
                        }
                        product.UpdateApplicationUserId = id;
                        product.UpdateDate = DateTime.Now;
                        productRepository.Update(product);
                        if (System.IO.File.Exists(fileDelete))
                        {
                            System.IO.File.Delete(fileDelete);
                        }
                        
                    }

                }

                return new OkResult();

            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na tentativa: ", ex.Message));
            }

        }

        [HttpGet()]
        [Route("getProductStatus")]
        [Authorize()]
        public IActionResult GetProductStatus()
        {
            try
            {
                return new JsonResult(productStatusRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet()]
        [Route("getProductType")]
        [Authorize()]
        public IActionResult GetProductType()
        {
            return new JsonResult(productTypeRepository.GetAll().ToList());
        }

        [HttpGet()]
        [Route("getAllProduct")]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(productRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet()]
        [Route("getPromotionSpotlight")]
        public IActionResult GetPromotionSpotlight()
        {
            try
            {
                Expression<Func<Product, bool>> p2, p1, p3;
                var predicate = PredicateBuilder.New<Product>();
                p1 = p => p.Active.Equals(true);
                predicate = predicate.And(p1);
                p2 = p => p.IsPromotion.Equals(true);
                predicate = predicate.And(p2);
                p3 = p => p.IsSpotlight.Equals(true);
                predicate = predicate.And(p3);
                return new JsonResult(productRepository.Where(predicate).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(productRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        [HttpPost()]
        [Route("getDetails")]
        public IActionResult GetDetails(FilterDefault filter)
        {
            return new JsonResult(productRepository.Get(filter.Id));
        }
    }
}
