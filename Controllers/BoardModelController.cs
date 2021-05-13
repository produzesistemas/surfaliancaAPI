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
        private IRepository<Litigation> litigationRepository;
        private IRepository<BoardModel> genericRepository;
        private IBoardModelRepository<BoardModel> boardModelRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;
        private IRepository<BoardModelShaper> boardModelShaperRepository;
        private IRepository<BoardModelBoardType> boardModelBoardTypeRepository;
        private IRepository<BoardModelBottom> boardModelBottomRepository;
        private IRepository<BoardModelConstruction> boardModelConstructionRepository;
        private IRepository<BoardModelFinSystem> boardModelFinSystemRepository;
        private IRepository<BoardModelLamination> boardModelLaminationRepository;
        private IRepository<BoardModelLitigation> boardModelLitigationRepository;
        private IRepository<BoardModelSize> boardModelSizeRepository;
        private IRepository<BoardModelTail> boardModelTailRepository;
        private IRepository<BoardModelWidth> boardModelWidthRepository;

        private readonly ApplicationDbContext _context;


        public BoardModelController(
    IRepository<Size> sizeRepository,
    IRepository<Width> widthRepository,
    IRepository<Litigation> litigationRepository,
                IWebHostEnvironment environment,
            IConfiguration Configuration,
            IRepository<BoardModel> genericRepository,
            IBoardModelRepository<BoardModel> boardModelRepository,
                IRepository<BoardModelShaper> boardModelShaperRepository,
            IRepository<BoardModelBoardType> boardModelBoardTypeRepository,
         IRepository<BoardModelBottom> boardModelBottomRepository,
         IRepository<BoardModelConstruction> boardModelConstructionRepository,
         IRepository<BoardModelFinSystem> boardModelFinSystemRepository,
         IRepository<BoardModelLamination> boardModelLaminationRepository,
         IRepository<BoardModelLitigation> boardModelLitigationRepository,
         IRepository<BoardModelSize> boardModelSizeRepository,
         IRepository<BoardModelTail> boardModelTailRepository,
         IRepository<BoardModelWidth> boardModelWidthRepository, ApplicationDbContext context
    )
        {
            this.sizeRepository = sizeRepository;
            this.widthRepository = widthRepository;
            this.litigationRepository = litigationRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.boardModelShaperRepository = boardModelShaperRepository;
            this.boardModelBoardTypeRepository = boardModelBoardTypeRepository;
            this.boardModelBottomRepository = boardModelBottomRepository;
            this.boardModelConstructionRepository = boardModelConstructionRepository;
            this.boardModelFinSystemRepository = boardModelFinSystemRepository;
            this.boardModelLaminationRepository = boardModelLaminationRepository;
            this.boardModelLitigationRepository = boardModelLitigationRepository;
            this.boardModelSizeRepository = boardModelSizeRepository;
            this.boardModelTailRepository = boardModelTailRepository;
            this.boardModelWidthRepository = boardModelWidthRepository;
            this.boardModelRepository = boardModelRepository;
            _context = context;
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
        [Route("getLitigations")]
        [Authorize()]
        public IActionResult GetLitigations(FilterDefault filter)
        {
            Expression<Func<Litigation, bool>> p2;
            var predicate = PredicateBuilder.New<Litigation>();
            if (filter.Name != null)
            {
                p2 = p => p.Description.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(litigationRepository.Where(predicate).ToList());
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
                    var boardModelBase = genericRepository.Get(boardModel.Id);
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[0].CopyTo(stream);
                        }
                        fileDelete = string.Concat(fileDelete, boardModelBase.ImageName);
                        boardModelBase.ImageName = fileName;
                    }

                    boardModelBase.Description = boardModel.Description;
                    boardModelBase.Value = boardModel.Value;
                    boardModelBase.UpdateApplicationUserId = id;
                    boardModelBase.UpdateDate = DateTime.Now;
                    genericRepository.Update(boardModelBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }

                }
                else
                {
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                            var extension = Path.GetExtension(files[0].FileName);
                            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                            var fullPath = Path.Combine(pathToSave, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files[0].CopyTo(stream);
                            }
                        boardModel.ImageName = fileName;
                        boardModel.ApplicationUserId = id;
                        boardModel.CreateDate = DateTime.Now;
                        genericRepository.Insert(boardModel);
                    }




                    //boardModel.BoardModelShapers.ForEach(bms =>
                    //{
                    //    bms.BoardModelId = boardModel.Id;
                    //    boardModelShaperRepository.Insert(bms);
                    //});
                    //boardModel.BoardModelBoardTypes.ForEach(bmt =>
                    //{
                    //    bmt.BoardModelId = boardModel.Id;
                    //    boardModelBoardTypeRepository.Insert(bmt);
                    //});
                    //boardModel.BoardModelBottoms.ForEach(bmb =>
                    //{
                    //    bmb.BoardModelId = boardModel.Id;
                    //    boardModelBottomRepository.Insert(bmb);
                    //});
                    //boardModel.BoardModelConstructions.ForEach(bmc =>
                    //{
                    //    bmc.BoardModelId = boardModel.Id;
                    //    boardModelConstructionRepository.Insert(bmc);
                    //});
                    //boardModel.BoardModelFinSystems.ForEach(bmf =>
                    //{
                    //    bmf.BoardModelId = boardModel.Id;
                    //    boardModelFinSystemRepository.Insert(bmf);
                    //});
                    //boardModel.BoardModelLaminations.ForEach(bml =>
                    //{
                    //    bml.BoardModelId = boardModel.Id;
                    //    boardModelLaminationRepository.Insert(bml);
                    //});
                    //boardModel.BoardModelLitigations.ForEach(bmli =>
                    //{
                    //    bmli.BoardModelId = boardModel.Id;
                    //    boardModelLitigationRepository.Insert(bmli);
                    //});
                    //boardModel.BoardModelSizes.ForEach(bmls =>
                    //{
                    //    bmls.BoardModelId = boardModel.Id;
                    //    boardModelSizeRepository.Insert(bmls);
                    //});
                    //boardModel.BoardModelTails.ForEach(bmlt =>
                    //{
                    //    bmlt.BoardModelId = boardModel.Id;
                    //    boardModelTailRepository.Insert(bmlt);
                    //});
                    //boardModel.BoardModelWidths.ForEach(bmlw =>
                    //{
                    //    bmlw.BoardModelId = boardModel.Id;
                    //    boardModelWidthRepository.Insert(bmlw);
                    //});


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
                    p2 = p => p.Description.Contains(filter.Name);
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

        class EqualityComparer : IEqualityComparer<BoardModelBoardType>
        {
            public bool Equals(BoardModelBoardType x, BoardModelBoardType y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId 
                    && x.BoardTypeId == y.BoardTypeId
                    && x.Id == y.Id;
            }

            public int GetHashCode(BoardModelBoardType obj)
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
                return new JsonResult(genericRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
