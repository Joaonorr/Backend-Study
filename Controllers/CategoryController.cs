using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("CategoriesProducts")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return _uow.CategoryRepository.GetCategoriesProducts().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return _uow.CategoryRepository.GetAll().ToList();
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public ActionResult<Category> Get(int id)
        {
            var category = _uow.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {

            if (category is null)
                return BadRequest();

            _uow.CategoryRepository.Add(category);
            _uow.Commit();

            return new CreatedAtRouteResult
                ("GetCategoryById", new {id = category.CategoryId}, category);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (category is null)
                return BadRequest();

            _uow.CategoryRepository.Update(category);
            _uow.Commit();

            return new CreatedAtRouteResult
                ("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _uow.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            _uow.CategoryRepository.Delete(category);
            _uow.Commit();

            return Ok(category);
        }


    }
}
