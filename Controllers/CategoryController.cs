using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("CategoriesProducts")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProductsAsync()
        {
            return await _context.Categories.Include(c => c.products).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetAsync(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {

            if (category is null)
                return BadRequest();

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult
                ("GetCategoryById", new {id = category.CategoryId}, category);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if (category is null)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return new CreatedAtRouteResult
                ("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }


    }
}
