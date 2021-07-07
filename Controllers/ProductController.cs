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
        private IRepository<Product> genericRepository;
        private IRepository<TypeSale> typeSaleRepository;
        private IRepository<ProductStatus> productStatusRepository;
        private IRepository<ProductType> productTypeRepository;
        private IProductRepository<Product> productRepository;

        public ProductController(
           IWebHostEnvironment environment,
           IConfiguration Configuration,
           IRepository<Product> genericRepository,
           IProductRepository<Product> productRepository,
           IRepository<TypeSale> typeSaleRepository,
        IRepository<ProductStatus> productStatusRepository,
        IRepository<ProductType> productTypeRepository
           )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.productRepository = productRepository;
            this.typeSaleRepository = typeSaleRepository;
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
            return new JsonResult(genericRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("getByProductType")]
        public IActionResult GetByProductType(FilterDefault filter)
        {
            Expression<Func<Product, bool>> p2, p1;
            var predicate = PredicateBuilder.New<Product>();
            p1 = p => p.Active.Equals(true);
            predicate = predicate.And(p1);
            if (filter.ProductTypeId > decimal.Zero)
            {
                p2 = p => p.ProductTypeId == filter.ProductTypeId;
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
                var product = JsonConvert.DeserializeObject<Product>(Convert.ToString(Request.Form["product"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileProduct"]);
                var fileDelete = pathToSave;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (product != null))
                {
                    if (product.Id == decimal.Zero)
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
                            product.ImageName = fileName;
                            product.ApplicationUserId = id;
                            product.CreateDate = DateTime.Now;
                            product.Active = true;
                            genericRepository.Insert(product);
                            return new OkResult();
                        }
                    }
                    else
                    {
                        var productBase = genericRepository.Get(product.Id);
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            var extension = Path.GetExtension(Request.Form.Files[0].FileName);
                            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                            using (var stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
                            {
                                Request.Form.Files[0].CopyTo(stream);
                            }
                            fileDelete = string.Concat(fileDelete, productBase.ImageName);
                            productBase.ImageName = fileName;
                        }
                        productBase.Description = product.Description;
                        productBase.Name = product.Name;
                        productBase.Value = product.Value;
                        productBase.ProductStatusId = product.ProductStatusId;
                        productBase.ProductTypeId = product.ProductTypeId;
                        productBase.IsPromotion = product.IsPromotion;
                        productBase.IsSpotlight = product.IsSpotlight;
                        if ((product.IsPromotion) && (product.ValuePromotion.HasValue))
                        {
                            productBase.ValuePromotion = product.ValuePromotion.Value;
                        }
                        productBase.UpdateApplicationUserId = id;
                        productBase.UpdateDate = DateTime.Now;
                        genericRepository.Update(productBase);
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
                return new JsonResult(genericRepository.GetAll().ToList());
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
                return new JsonResult(genericRepository.Get(id));
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
