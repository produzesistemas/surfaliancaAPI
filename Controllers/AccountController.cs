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
using System.Collections.Generic;
using System.Security.Claims;

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
        [Route("registerClient")]
        public async Task<IActionResult> RegisterClient(ApplicationUser user)
        {
            try
            {
                var exist = await userManager.FindByEmailAsync(user.Email);
                if (exist == null)
                {
                    user.UserName = user.Email.Split("@").FirstOrDefault();
                    var result = await userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        List<string> permissions = new List<string>();
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Role, "Cliente"));
                        await userManager.AddClaimsAsync(user, claims);
                        permissions.Add("Cliente");
                        user.Token = TokenService.GenerateToken(user, configuration, permissions);
                        user.PasswordHash = null;
                        return new JsonResult(user);
                    }
                } else
                {
                    var claimscurrentUser = userManager.GetClaimsAsync(exist).Result.ToList();

                    //var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user);
                    //var claims = claimsPrincipal.Claims.ToList();
                    var permissions = claimscurrentUser.Where(c => c.Type.Contains("role")).Select(c => c.Value).ToList();
                    var applicationUser = new ApplicationUser();
                    applicationUser = (ApplicationUser)exist;
                    applicationUser.Token = TokenService.GenerateToken(applicationUser, configuration, permissions);
                    var applicationUserDTO = new ApplicationUserDTO();
                    applicationUserDTO.Token = applicationUser.Token;
                    applicationUserDTO.Id = applicationUser.Id;
                    applicationUserDTO.Provider = applicationUser.Provider;
                    applicationUserDTO.ProviderId = applicationUser.ProviderId;
                    applicationUserDTO.Email = applicationUser.Email;
                    applicationUserDTO.UserName = applicationUser.UserName;
                    applicationUserDTO.Role = permissions.FirstOrDefault();
                    return new JsonResult(applicationUserDTO);
                }


            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }


            return new JsonResult(user);

        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Secret, false, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Acesso negado! Login inválido!");
                }
                var user = await userManager.FindByEmailAsync(loginUser.Email);

                var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user);
                var claims = claimsPrincipal.Claims.ToList();
                var permissions = claims.Where(c => c.Type.Contains("role")).Select(c => c.Value).ToList();
                if (!permissions.Where(x => x.Contains("Master")).Any())
                {
                    return BadRequest("Acesso negado! Usuário não é Master!");
                }
                var applicationUser = new ApplicationUser();
                applicationUser.Id = user.Id;
                var applicationUserDTO = new ApplicationUserDTO();
                applicationUserDTO.Id = user.Id;
                applicationUserDTO.Token = TokenService.GenerateToken(applicationUser, configuration, permissions);
                applicationUserDTO.Email = user.Email;
                applicationUserDTO.UserName = user.UserName;
                applicationUserDTO.Role = permissions.FirstOrDefault();
                return new JsonResult(applicationUserDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Falha no login! " + ex.Message);
            }

        }

        [HttpPost()]
        [AllowAnonymous]
        //[Authorize()]
        [Route("registerMaster")]
        public async Task<IActionResult> RegisterMaster(LoginUser loginUser)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = loginUser.Email,
                    Email = loginUser.Email
                };
                var result = await userManager.CreateAsync(user, loginUser.Secret);
                if (result.Succeeded)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Role, "Master"));
                    await userManager.AddClaimsAsync(user, claims);

                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault().Description);
                }
                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }

        }

        [HttpGet()]
        [Route("getClients")]
        [Authorize()]
        public IActionResult GetClients()
        {
            try
            {
                Claim claim = new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Cliente");
                var users = userManager.GetUsersForClaimAsync(claim).Result.ToList();
                return new JsonResult(users);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no carregamento dos usuários: ", ex.Message));
            }
        }
    }
}
