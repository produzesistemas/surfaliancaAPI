using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private IStateRepository genericRepository;

        public StateController(
            IStateRepository genericRepository
    )
        {
            this.genericRepository = genericRepository;
        }

        [HttpGet()]
        [Route("getAll")]
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
        
    }
}

