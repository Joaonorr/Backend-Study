using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("CategoriesProducts")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var categories =  _uow.CategoryRepository.GetCategoriesProducts().ToList();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = _uow.CategoryRepository.GetAll().ToList();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _uow.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDTO);
        }

        [HttpPost]
        public ActionResult<Category> Post(CategoryDTO categoryDTO)
        {

            if (categoryDTO is null)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryDTO);

            _uow.CategoryRepository.Add(category);
            _uow.Commit();

            return new CreatedAtRouteResult
                ("GetCategoryById", new {id = categoryDTO.CategoryId}, categoryDTO);

        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryDTO);

            _uow.CategoryRepository.Update(category);
            _uow.Commit();

            return new CreatedAtRouteResult
                ("GetCategoryById", new { id = categoryDTO.CategoryId }, categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _uow.CategoryRepository.Get(c => c.CategoryId == id);

            if (category is null)
                return NotFound("Category Not Found");

            _uow.CategoryRepository.Delete(category);
            _uow.Commit();

            return Ok();
        }


    }
}
