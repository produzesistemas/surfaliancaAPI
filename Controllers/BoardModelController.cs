using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardModelController : ControllerBase
    {
        private IBoardModelRepository boardModelRepository;
        private IBoardModelDimensionsRepository boardModelDimensionsRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public BoardModelController(
                IWebHostEnvironment environment,
            IConfiguration Configuration,
            IBoardModelRepository boardModelRepository, IBoardModelDimensionsRepository boardModelDimensionsRepository
    )
        {
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.boardModelRepository = boardModelRepository;
            this.boardModelDimensionsRepository = boardModelDimensionsRepository;
        }


        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var boardModel = JsonConvert.DeserializeObject<BoardModel>(Convert.ToString(Request.Form["boardModel"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;

                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }

                var files = Request.Form.Files;
                if (boardModel.Id > decimal.Zero)
                {
                    var boardModelBase = boardModelRepository.Get(boardModel.Id);
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
                                boardModelBase.ImageName = fileName;
                                break;
                        }

                    };

                    boardModelBase.Description = boardModel.Description;
                    boardModelBase.Name = boardModel.Name;
                    boardModelBase.Value = boardModel.Value;
                    boardModelBase.DaysProduction = boardModel.DaysProduction;
                    boardModelBase.UrlMovie = boardModel.UrlMovie;
                    boardModelBase.UpdateApplicationUserId = id;
                    boardModelBase.UpdateDate = DateTime.Now;
                    boardModelRepository.Update(boardModelBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }
                    boardModelBase.BoardModelDimensions.ForEach(x =>
                    {
                        boardModelDimensionsRepository.Delete(x.Id);
                    });
                    boardModel.BoardModelDimensions.ForEach(boardModelDimensions =>
                    {
                        boardModelDimensions.BoardModelId = boardModel.Id;
                        boardModelDimensionsRepository.Insert(boardModelDimensions);
                    });

                }
                else
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
                                boardModel.ImageName = fileName;
                                break;
                        }

                    };

                        boardModel.ApplicationUserId = id;
                        boardModel.CreateDate = DateTime.Now;
                        boardModel.Active = true;
                    boardModelRepository.Insert(boardModel);
                      
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na tentativa: ", ex.Message));
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
            try
            {
                Expression<Func<BoardModel, bool>> p2,p1;
                var predicate = PredicateBuilder.New<BoardModel>();
                if (filter.Name != null)
                {
                    p2 = p => p.Name.Contains(filter.Name);
                    predicate = predicate.And(p2);
                }

                return new JsonResult(boardModelRepository.Where(predicate).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest("Faha no carregamento: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                var board = boardModelRepository.Get(id);
                return new JsonResult(board);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível carregar o model: " + ex.Message);
            }
        }

        [HttpPost()]
        [Route("getToOrder")]
        public IActionResult GetToOrder(BoardModel boardModel)
        {
            try
            {
                return new JsonResult(boardModelRepository.Get(boardModel.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
           
        }


        [HttpGet()]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(boardModelRepository.Where(x => x.Active == true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                //var entityBase = genericRepository.Get(id);
                boardModelRepository.Delete(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha na exclusão do modelo: ", ex.Message));

            }
        }

        [HttpPost()]
        [Route("active")]
        [Authorize()]
        public IActionResult Active(BoardModel boardModel)
        {
            try
            {
                boardModelRepository.Active(boardModel.Id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}

