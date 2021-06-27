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
        private IRepository<BoardModelBoardType> boardModelBoardTypeRepository;
        private IRepository<BoardModelBottom> boardModelBottomRepository;
        private IRepository<BoardModelSize> boardModelSizeRepository;
        private IRepository<BoardModelWidth> boardModelWidthRepository;
        private IRepository<BoardModelLamination> boardModelLaminationRepository;
        private IRepository<BoardModelConstruction> boardModelConstructionRepository;
        private IRepository<BoardModelShaper> boardModelShaperRepository;
        private IRepository<BoardModelTail> boardModelTailRepository;


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
            IRepository<BoardModelBoardType> boardModelBoardTypeRepository,
            IRepository<BoardModelBottom> boardModelBottomRepository,
            IRepository<BoardModelSize> boardModelSizeRepository,
            IRepository<BoardModelWidth> boardModelWidthRepository,
            IRepository<BoardModelLamination> boardModelLaminationRepository,
            IRepository<BoardModelConstruction> boardModelConstructionRepository,
            IRepository<BoardModelShaper> boardModelShaperRepository,
            IRepository<BoardModelTail> boardModelTailRepository,
            IBoardModelRepository<BoardModel> boardModelRepository
    )
        {
            this.sizeRepository = sizeRepository;
            this.widthRepository = widthRepository;
            this.paintRepository = paintRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
            this.genericRepository = genericRepository;
            this.boardModelBoardTypeRepository = boardModelBoardTypeRepository;
            this.boardModelBottomRepository = boardModelBottomRepository;
            this.boardModelSizeRepository = boardModelSizeRepository;
            this.boardModelWidthRepository = boardModelWidthRepository;
            this.boardModelLaminationRepository = boardModelLaminationRepository;
            this.boardModelConstructionRepository = boardModelConstructionRepository;
            this.boardModelShaperRepository = boardModelShaperRepository;
            this.boardModelTailRepository = boardModelTailRepository;
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
                    boardModelBase.Name = boardModel.Name;
                    boardModelBase.Value = boardModel.Value;
                    boardModelBase.DaysProduction = boardModel.DaysProduction;
                    boardModelBase.UpdateApplicationUserId = id;
                    boardModelBase.UpdateDate = DateTime.Now;
                    boardModelRepository.Update(boardModelBase);
                    if (System.IO.File.Exists(fileDelete))
                    {
                        System.IO.File.Delete(fileDelete);
                    }

                    var toDelete = boardModelBase.BoardModelBoardTypes.Except(boardModel.BoardModelBoardTypes, new EqualityComparerBoardModelBoardType()).ToList();
                    var toInsert = boardModel.BoardModelBoardTypes.Except(boardModelBase.BoardModelBoardTypes, new EqualityComparerBoardModelBoardType()).ToList();
                    toDelete.ForEach(x =>
                    {
                        boardModelBoardTypeRepository.Delete(x);
                    });
                    toInsert.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelBoardTypeRepository.Insert(x);
                    });

                    var toDeleteBottom = boardModelBase.BoardModelBottoms.Except(boardModel.BoardModelBottoms, new EqualityComparerBoardModelBottom()).ToList();
                    var toInsertBottom = boardModel.BoardModelBottoms.Except(boardModelBase.BoardModelBottoms, new EqualityComparerBoardModelBottom()).ToList();
                    toDeleteBottom.ForEach(x =>
                    {
                        boardModelBottomRepository.Delete(x);
                    });
                    toInsertBottom.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelBottomRepository.Insert(x);
                    });

                    var toDeleteConstruction = boardModelBase.BoardModelConstructions.Except(boardModel.BoardModelConstructions, new EqualityComparerBoardModelConstruction()).ToList();
                    var toInsertConstruction = boardModel.BoardModelConstructions.Except(boardModelBase.BoardModelConstructions, new EqualityComparerBoardModelConstruction()).ToList();
                    toDeleteConstruction.ForEach(x =>
                    {
                        boardModelConstructionRepository.Delete(x);
                    });
                    toInsertConstruction.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelConstructionRepository.Insert(x);
                    });

                    var toDeleteLamination = boardModelBase.BoardModelLaminations.Except(boardModel.BoardModelLaminations, new EqualityComparerBoardModelLamination()).ToList();
                    var toInsertLamination = boardModel.BoardModelLaminations.Except(boardModelBase.BoardModelLaminations, new EqualityComparerBoardModelLamination()).ToList();
                    toDeleteLamination.ForEach(x =>
                    {
                        boardModelLaminationRepository.Delete(x);
                    });
                    toInsertLamination.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelLaminationRepository.Insert(x);
                    });

                    var toDeleteShaper = boardModelBase.BoardModelShapers.Except(boardModel.BoardModelShapers, new EqualityComparerBoardModelShaper()).ToList();
                    var toInsertShaper = boardModel.BoardModelShapers.Except(boardModelBase.BoardModelShapers, new EqualityComparerBoardModelShaper()).ToList();
                    toDeleteShaper.ForEach(x =>
                    {
                        boardModelShaperRepository.Delete(x);
                    });
                    toInsertShaper.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelShaperRepository.Insert(x);
                    });

                    var toDeleteSize = boardModelBase.BoardModelSizes.Except(boardModel.BoardModelSizes, new EqualityComparerBoardModelSize()).ToList();
                    var toInsertSize = boardModel.BoardModelSizes.Except(boardModelBase.BoardModelSizes, new EqualityComparerBoardModelSize()).ToList();
                    toDeleteSize.ForEach(x =>
                    {
                        boardModelSizeRepository.Delete(x);
                    });
                    toInsertSize.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelSizeRepository.Insert(x);
                    });

                    var toDeleteTail = boardModelBase.BoardModelTails.Except(boardModel.BoardModelTails, new EqualityComparerBoardModelTail()).ToList();
                    var toInsertTail = boardModel.BoardModelTails.Except(boardModelBase.BoardModelTails, new EqualityComparerBoardModelTail()).ToList();
                    toDeleteTail.ForEach(x =>
                    {
                        boardModelTailRepository.Delete(x);
                    });
                    toInsertTail.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelTailRepository.Insert(x);
                    });

                    var toDeleteWidth = boardModelBase.BoardModelWidths.Except(boardModel.BoardModelWidths, new EqualityComparerBoardModelWidth()).ToList();
                    var toInsertWidth = boardModel.BoardModelWidths.Except(boardModelBase.BoardModelWidths, new EqualityComparerBoardModelWidth()).ToList();
                    toDeleteWidth.ForEach(x =>
                    {
                        boardModelWidthRepository.Delete(x);
                    });
                    toInsertWidth.ForEach(x =>
                    {
                        x.BoardModelId = boardModelBase.Id;
                        boardModelWidthRepository.Insert(x);
                    });

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
                        boardModel.Active = true;
                        genericRepository.Insert(boardModel);
                    }
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

        class EqualityComparerBoardModelBoardType : IEqualityComparer<BoardModelBoardType>
        {
            public bool Equals(BoardModelBoardType x, BoardModelBoardType y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId 
                    && x.BoardTypeId == y.BoardTypeId;
            }

            public int GetHashCode(BoardModelBoardType obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelBottom : IEqualityComparer<BoardModelBottom>
        {
            public bool Equals(BoardModelBottom x, BoardModelBottom y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.BottomId == y.BottomId;
            }

            public int GetHashCode(BoardModelBottom obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelConstruction : IEqualityComparer<BoardModelConstruction>
        {
            public bool Equals(BoardModelConstruction x, BoardModelConstruction y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.ConstructionId == y.ConstructionId;
            }

            public int GetHashCode(BoardModelConstruction obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelLamination : IEqualityComparer<BoardModelLamination>
        {
            public bool Equals(BoardModelLamination x, BoardModelLamination y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.LaminationId == y.LaminationId;
            }

            public int GetHashCode(BoardModelLamination obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelShaper : IEqualityComparer<BoardModelShaper>
        {
            public bool Equals(BoardModelShaper x, BoardModelShaper y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.ShaperId == y.ShaperId;
            }

            public int GetHashCode(BoardModelShaper obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelSize : IEqualityComparer<BoardModelSize>
        {
            public bool Equals(BoardModelSize x, BoardModelSize y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.SizeId == y.SizeId;
            }

            public int GetHashCode(BoardModelSize obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelTail : IEqualityComparer<BoardModelTail>
        {
            public bool Equals(BoardModelTail x, BoardModelTail y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.TailId == y.TailId;
            }

            public int GetHashCode(BoardModelTail obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        class EqualityComparerBoardModelWidth : IEqualityComparer<BoardModelWidth>
        {
            public bool Equals(BoardModelWidth x, BoardModelWidth y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                if (x == null || y == null)
                    return false;
                return x.BoardModelId == y.BoardModelId
                    && x.WidthId == y.WidthId;
            }

            public int GetHashCode(BoardModelWidth obj)
            {
                return obj.Id.GetHashCode();
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
                return new JsonResult(genericRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}
