using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Pagination;
using WebApplication1.Repository.UnitWork;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers;

[Produces("application/json")]
[Authorize(AuthenticationSchemes = "Bearer")]
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

    /// <summary>
    /// Get the categories and their respective products
    /// </summary>
    /// <param name="categoryParameters">Paged Parameters</param>
    /// <returns>categories and their respective products</returns>
    [HttpGet("CategoriesProducts")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts(
        [FromQuery] CategoryParameters categoryParameters
    )
    {
        var categories =  await _uow.CategoryRepository.GetCategoriesProducts(categoryParameters);
        var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
        return categoriesDTO;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll(
        [FromQuery] CategoryParameters categoryParameters
    )
    {
        var categories = await _uow.CategoryRepository.GetCategoriesPaged(categoryParameters);
        var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
        return categoriesDTO;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _uow.CategoryRepository.Get(c => c.CategoryId == id);

        if (category is null)
            return NotFound("Category Not Found");
        

        var categoryDTO = _mapper.Map<CategoryDTO>(category);

        return Ok(categoryDTO);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> Post(CategoryDTO categoryDTO)
    {

        if (categoryDTO is null)
            return BadRequest();

        var category = _mapper.Map<Category>(categoryDTO);

        _uow.CategoryRepository.Add(category);
        await _uow.Commit();

        return new CreatedAtRouteResult
            ("GetCategoryById", new {id = categoryDTO.CategoryId}, categoryDTO);

    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> Put(int id, CategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest();

        if (categoryDTO.CategoryId != id)
            return BadRequest();

        var category = _mapper.Map<Category>(categoryDTO);

        _uow.CategoryRepository.Update(category);
        await _uow.Commit();

        return new CreatedAtRouteResult
            ("GetCategoryById", new { id = categoryDTO.CategoryId }, categoryDTO);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> Delete(int id)
    {
        var category = await _uow.CategoryRepository.Get(c => c.CategoryId == id);

        if (category is null)
            return NotFound("Category Not Found");

        _uow.CategoryRepository.Delete(category);
        await _uow.Commit();

        return Ok();
    }


}
