using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private IRepository<FinColor> genericRepository;
        public ColorController(
    IRepository<FinColor> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        [HttpGet()]
        [Route("getColors")]
        [Authorize()]
        public IActionResult GetColors()
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
    }
}
