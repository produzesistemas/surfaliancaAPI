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
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqKit;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private ITeamImageRepository teamImageRepository;
        private ITeamRepository teamRepository;
        public TeamController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            ITeamImageRepository teamImageRepository,
            ITeamRepository teamRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.teamRepository = teamRepository;
            this.teamImageRepository = teamImageRepository;
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var team = JsonConvert.DeserializeObject<Team>(Convert.ToString(Request.Form["team"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileTeam"]);
                var fileDelete = pathToSave;
                var files = Request.Form.Files;
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id != null) || (team != null))
                {
                    if (team.Id == decimal.Zero)
                    {
                        if (Request.Form.Files.Count() > decimal.Zero)
                        {
                            team.ApplicationUserId = id;
                            team.CreateDate = DateTime.Now;
                            team.Active = true;
                            teamRepository.Insert(team);
                            var filesUpload = Request.Form.Files;

                            for (var counter = 0; counter < files.Count; counter++)
                            {
                                var extension = Path.GetExtension(files[counter].FileName);
                                var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                                var fullPath = Path.Combine(pathToSave, fileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    files[counter].CopyTo(stream);
                                }
                                teamImageRepository.Insert(new TeamImage() { ImageName = fileName, TeamId = team.Id });
                            };
                        }
                    } else 
                    {
                        var teamBase = teamRepository.Get(team.Id);
                        //var lstteamImageBase = teamImageRepository.Where(w => w.TeamId == team.Id).ToList();
                        teamBase.Description = team.Description;
                        teamBase.Name = team.Name;
                        teamBase.UpdateApplicationUserId = id;
                        teamBase.UpdateDate = DateTime.Now;
                        teamRepository.Update(teamBase);
                        teamBase.teamImages.ForEach(x =>
                        {
                            teamImageRepository.Delete(x.Id);
                        });
                        for (var counter = 0; counter < files.Count; counter++)
                        {
                            var extension = Path.GetExtension(files[counter].FileName);
                            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                            var fullPath = Path.Combine(pathToSave, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files[counter].CopyTo(stream);
                            }
                            teamImageRepository.Insert(new TeamImage() { ImageName = fileName, TeamId = teamBase.Id });
                        };
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
                return new JsonResult(teamRepository.GetAll().ToList());

        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                teamRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(teamRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }

        class EqualityComparer : IEqualityComparer<TeamImage>
        {
            public bool Equals(TeamImage x, TeamImage y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.TeamId == y.TeamId && x.ImageName == y.ImageName;
            }

            public int GetHashCode(TeamImage obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        [HttpGet()]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(teamRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


    }
}
