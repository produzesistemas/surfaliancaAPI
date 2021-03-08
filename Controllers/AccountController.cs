using System;
using System.Threading.Tasks;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using UnitOfWork;
using System.Linq;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private IConfiguration configuration;
        private IRepository<Store> storeRepository;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IRepository<Store> storeRepository,
            IConfiguration Configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = Configuration;
            this.storeRepository = storeRepository;
        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("registerPartner")]
        public async Task<IActionResult> RegisterPartner(ApplicationUser user)
        {
            try
            {
                var exist = await userManager.FindByEmailAsync(user.Email);
                if (exist == null)
                {
                    user.UserName = user.Email;
                    var result = await userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Parceiro");
                        user.PasswordHash = null;
                        var store = storeRepository.Where(x => x.AspNetUsersId == user.Id).FirstOrDefault();
                        if (store == null)
                        {
                            store = new Store() { Id = 0 };
                        }
                        user.Token = TokenService.GenerateToken(user, configuration, store);
                        return new JsonResult(user);
                    }
                } else
                {
                    var applicationUser = new ApplicationUser();
                    applicationUser = (ApplicationUser)exist;
                    var store = storeRepository.Where(x => x.AspNetUsersId == applicationUser.Id).FirstOrDefault();
                    if (store == null)
                    {
                        store = new Store() { Id = 0 };
                    }
                    user.Token = TokenService.GenerateToken(user, configuration, store);
                    applicationUser.Token = TokenService.GenerateToken(applicationUser, configuration, store);
                    return new JsonResult(exist);
                }


            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }


            return new JsonResult(user);

        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("login")]
        //public async Task<IActionResult> Login(ApplicationUser user)
        //{
            
        //}
    }
}
