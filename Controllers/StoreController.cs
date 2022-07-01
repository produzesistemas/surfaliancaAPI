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
        private IStoreRepository storeRepository;
        public StoreController(
            IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;

        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save(Store store)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                if (store.Id > decimal.Zero)
                {
                    store.UpdateApplicationUserId = id;
                    store.UpdateDate = DateTime.Now;
                    storeRepository.Update(store);
                }
                else
                {
                    store.CreateDate = DateTime.Now;
                    storeRepository.Insert(store);
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
