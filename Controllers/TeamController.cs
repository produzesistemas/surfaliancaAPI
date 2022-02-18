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
        private IRepository<Team> genericRepository;
        private IRepository<TeamImage> teamImageRepository;
        private ITeamRepository<Team> teamRepository;
        private IStoreRepository<Store> storeRepository;
        public TeamController(UserManager<IdentityUser> userManager,
            IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<Team> genericRepository,
            IStoreRepository<Store> storeRepository,
            ITeamRepository<Team> teamRepository,
            IRepository<TeamImage> teamImageRepository)
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.teamRepository = teamRepository;
            this.teamImageRepository = teamImageRepository;
            this.storeRepository = storeRepository;
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
                            genericRepository.Insert(team);
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
                        var teamBase = genericRepository.Get(team.Id);
                        var lstteamImageBase = teamImageRepository.Where(w => w.TeamId == team.Id).ToList();
                        teamBase.Description = team.Description;
                        teamBase.Name = team.Name;
                        teamBase.UpdateApplicationUserId = id;
                        teamBase.UpdateDate = DateTime.Now;
                        genericRepository.Update(teamBase);
                        lstteamImageBase.ForEach(x =>
                        {
                            teamImageRepository.Delete(x);
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
                Expression<Func<Team, bool>> p2;
                var predicate = PredicateBuilder.New<Team>();
                if (filter.Name != null)
                {
                    p2 = p => p.Name.Contains(filter.Name);
                    predicate = predicate.And(p2);
                }
                return new JsonResult(genericRepository.Where(predicate).ToList());

        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                var teamBase = teamRepository.Get(id);
                teamBase.teamImages.ForEach(image =>
                {
                    var imageDelete = teamImageRepository.Get(image.Id);
                    teamImageRepository.Delete(imageDelete);
                });
                genericRepository.Delete(teamBase);
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
                var teams = genericRepository.GetAll().ToList();
                teams.ForEach(team => {
                    team.teamImages = teamImageRepository.Where(x => x.TeamId == team.Id).ToList();

                });

                return new JsonResult(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


    }
}
