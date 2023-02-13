using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Pagination;
using WebApplication1.Repository.UnitWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductsParameters productsParameters)
        {
            var products = await _uow.ProductRepository.GetProductsPaged(productsParameters);

            if (products is null)
                return NotFound("Empty product list");

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _uow.ProductRepository.Get(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found");

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }


        [HttpPost]
        public async Task<ActionResult> Post(ProductDTO productDTO)
        {

            if (productDTO is null)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _uow.ProductRepository.Add(product);
            await _uow.Commit();

            return new CreatedAtRouteResult("GetProductById",
                new {id = productDTO.ProductId}, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProductDTO productDTO)
        {
            if (id != productDTO.ProductId)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _uow.ProductRepository.Update(product);
            await _uow.Commit();

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _uow.ProductRepository.Get(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found");

            _uow.ProductRepository.Delete(product);
            await _uow.Commit();

            return Ok();
        }
    }
}
