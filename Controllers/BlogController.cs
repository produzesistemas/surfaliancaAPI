using System;
using System.Linq;
using System.Security.Claims;
using Models;
using UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using LinqKit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogRepository blogRepository;
        private ITypeBlogRepository typeBlogRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IConfiguration _configuration;

        public BlogController(
                            IWebHostEnvironment environment,
                            ITypeBlogRepository typeBlogRepository,
            IConfiguration Configuration,
             IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
            this.typeBlogRepository = typeBlogRepository;
            _hostEnvironment = environment;
            _configuration = Configuration;
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
            Expression<Func<Blog, bool>> p2;
            var predicate = PredicateBuilder.New<Blog>();
            if (filter.Name != null)
            {
                p2 = p => p.Description.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(blogRepository.Where(predicate).ToList());
        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save()
        {
            try
            {
                var blog = JsonConvert.DeserializeObject<Blog>(Convert.ToString(Request.Form["blog"]));
                var pathToSave = string.Concat(_hostEnvironment.ContentRootPath, _configuration["pathFileStore"]);
                var fileDelete = pathToSave;

                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                var files = Request.Form.Files;

                if (blog.Id > decimal.Zero)
                {
                    var entityBase = blogRepository.Get(blog.Id);
                    if (Request.Form.Files.Count() > decimal.Zero)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var fileName = string.Concat(Guid.NewGuid().ToString(), extension);
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files[0].CopyTo(stream);
                        }
                        fileDelete = string.Concat(fileDelete, blog.ImageName);
                        entityBase.ImageName = fileName;
                    }
                    entityBase.UpdateApplicationUserId = id;
                    entityBase.UpdateDate = DateTime.Now;
                    entityBase.Description = blog.Description;
                    entityBase.Details = blog.Details;
                    blogRepository.Update(entityBase);
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
                        blog.ImageName = fileName;
                        blog.ApplicationUserId = id;
                        blog.CreateDate = DateTime.Now;
                        blogRepository.Insert(blog);
                    }
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public IActionResult Delete(int id)
        {
            try
            {
                blogRepository.Delete(id);
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
                return new JsonResult(blogRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Arquivo não encontrado!" + ex.Message);
            }
        }


        [HttpGet()]
        [Route("getAllTypeBlog")]
        [Authorize()]
        public IActionResult GetAllTypeBlog()
        {
            try
            {
                return new JsonResult(typeBlogRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost()]
        [Route("getAllByFilter")]
        public IActionResult GetAllByFilter(FilterDefault filter)
        {
            Expression<Func<Blog, bool>> p2;
            var predicate = PredicateBuilder.New<Blog>();
            if (filter.Name != null)
            {
                p2 = p => p.Description.Contains(filter.Name);
                predicate = predicate.And(p2);
            }
            return new JsonResult(blogRepository.GetAll().ToList());
        }

    }
}
