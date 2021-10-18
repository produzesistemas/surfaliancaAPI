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
        private IRepository<Size> sizeRepository;
        private IRepository<Width> widthRepository;
        private IRepository<Paint> paintRepository;
        private IRepository<BoardModel> genericRepository;
        private IRepository<BoardModelColors> BoardModelColorsRepository;
        private IRepository<BoardModelDimensions> BoardModelDimensionsRepository;
        private IBoardModelRepository<BoardModel> boardModelRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public BoardModelController(
    IRepository<Size> sizeRepository,
    IRepository<Width> widthRepository,
    IRepository<Paint> paintRepository,
                IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<BoardModel> genericRepository,
            IRepository<BoardModelColors> BoardModelColorsRepository,
            IRepository<BoardModelDimensions> BoardModelDimensionsRepository,
            IBoardModelRepository<BoardModel> boardModelRepository
    )
        {
            this.sizeRepository = sizeRepository;
            this.widthRepository = widthRepository;
            this.paintRepository = paintRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.BoardModelDimensionsRepository = BoardModelDimensionsRepository;
            this.BoardModelColorsRepository = BoardModelColorsRepository;
            this.boardModelRepository = boardModelRepository;
        }

        [HttpPost()]
        [Route("getSizes")]
        [Authorize()]
        public IActionResult GetSizes(FilterDefault filter)
        {
            Expression<Func<Size, bool>> p2;
            var predicate = PredicateBuilder.New<Size>();
            if (filter.Name != null)
            {
                p2 = p => p.Description.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(sizeRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("getWidths")]
        [Authorize()]
        public IActionResult GetWidths(FilterDefault filter)
        {
            Expression<Func<Width, bool>> p2;
            var predicate = PredicateBuilder.New<Width>();
            if (filter.Name != null)
            {
                p2 = p => p.Description.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(widthRepository.Where(predicate).ToList());
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
                            case 1:
                                boardModelBase.ImageName1 = fileName;
                                break;
                            case 2:
                                boardModelBase.ImageName2 = fileName;
                                break;
                            case 3:
                                boardModelBase.ImageName3 = fileName;
                                break;
                        }

                    };

                    boardModelBase.Description = boardModel.Description;
                    boardModelBase.Name = boardModel.Name;
                    boardModelBase.Value = boardModel.Value;
                    boardModelBase.DaysProduction = boardModel.DaysProduction;
                    boardModelBase.BoardTypeId = boardModel.BoardTypeId;

                    boardModelBase.UrlMovie = boardModel.UrlMovie;
                    boardModelBase.UpdateApplicationUserId = id;
                    boardModelBase.UpdateDate = DateTime.Now;
                    boardModelRepository.Update(boardModelBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }
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
                            case 1:
                                boardModel.ImageName1 = fileName;
                                break;
                            case 2:
                                boardModel.ImageName2 = fileName;
                                break;
                            case 3:
                                boardModel.ImageName3 = fileName;
                                break;
                        }

                    };

                        boardModel.ApplicationUserId = id;
                        boardModel.CreateDate = DateTime.Now;
                        boardModel.Active = true;
                        genericRepository.Insert(boardModel);
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
                Expression<Func<BoardModel, bool>> p2;
                var predicate = PredicateBuilder.New<BoardModel>();
                if (filter.Name != null)
                {
                    p2 = p => p.Name.Contains(filter.Name);
                    predicate = predicate.And(p2);
                }
                return new JsonResult(genericRepository.Where(predicate).ToList());
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

        //class EqualityComparerBoardModelConstruction : IEqualityComparer<BoardModelConstruction>
        //{
        //    public bool Equals(BoardModelConstruction x, BoardModelConstruction y)
        //    {
        //        if (object.ReferenceEquals(x, y))
        //            return true;
        //        if (x == null || y == null)
        //            return false;
        //        return x.BoardModelId == y.BoardModelId
        //            && x.ConstructionId == y.ConstructionId;
        //    }

        //    public int GetHashCode(BoardModelConstruction obj)
        //    {
        //        return obj.Id.GetHashCode();
        //    }
        //}
        //class EqualityComparerBoardModelLamination : IEqualityComparer<BoardModelLamination>
        //{
        //    public bool Equals(BoardModelLamination x, BoardModelLamination y)
        //    {
        //        if (object.ReferenceEquals(x, y))
        //            return true;
        //        if (x == null || y == null)
        //            return false;
        //        return x.BoardModelId == y.BoardModelId
        //            && x.LaminationId == y.LaminationId;
        //    }

        //    public int GetHashCode(BoardModelLamination obj)
        //    {
        //        return obj.Id.GetHashCode();
        //    }
        //}

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
                return new JsonResult(genericRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
