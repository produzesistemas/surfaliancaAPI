using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LinqKit;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductModelController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<ProductModel> genericRepository;
        private IRepository<Store> storeRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ProductModelController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<ProductModel> productModelRepository,
            IRepository<Store> storeRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = productModelRepository;
            this.storeRepository = storeRepository;
            this.userManager = userManager;
        }

        [HttpPost()]
        [Route("filter")]
        [Authorize()]
        public IActionResult GetByFilter(FilterDefault filter)
        {
            ClaimsPrincipal currentUser = this.User;
            var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
            var storeId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(z => z.Type.Contains("sid")).Value);
            if (id == null)
            {
                return BadRequest("Identificação do usuário não encontrada.");
            }
            Expression<Func<ProductModel, bool>> p1, p2;
            var predicate = PredicateBuilder.New<ProductModel>();
            p1 = p => p.StoreId == storeId;
            predicate = predicate.And(p1);
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
        public async Task<IActionResult> Save(ProductModel productModel)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserName = currentUser.Claims.First().Value;
                var user = await userManager.FindByEmailAsync(currentUserName);
                if (user != null)
                {
                    var store = storeRepository.Where(x => x.AspNetUsersId == user.Id).FirstOrDefault();
                    if (store == null)
                    {
                        return BadRequest("Loja não encontrada para o usuário. Cadastre uma loja clicando no botão de configurações");
                    }

                    if (productModel.Id > decimal.Zero)
                    {
                        var productModelBase = genericRepository.Get(productModel.Id);
                        productModelBase.Name = productModel.Name;
                        genericRepository.Update(productModelBase);
                    } else
                    {
                        productModel.StoreId = store.Id;
                        genericRepository.Insert(productModel);
                    }
                    return new JsonResult(productModel);
                } else
                {
                    return BadRequest("Usuário não encontrado! Efetue o login.");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
            //return new JsonResult(productModel);

        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                var productModelBase = genericRepository.Get(id);
                genericRepository.Delete(productModelBase);
                return new OkResult();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }
    }
}
