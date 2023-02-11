using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repository;

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
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var products = _uow.ProductRepository.GetAll().ToList();

            if (products is null)
                return NotFound("Empty product list");

            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _uow.ProductRepository.Get(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found");

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }


        [HttpPost]
        public ActionResult Post(ProductDTO productDTO)
        {

            if (productDTO is null)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _uow.ProductRepository.Add(product);
            _uow.Commit();

            return new CreatedAtRouteResult("GetProductById",
                new {id = productDTO.ProductId}, productDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProductDTO productDTO)
        {
            if (id != productDTO.ProductId)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _uow.ProductRepository.Update(product);
            _uow.Commit();

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _uow.ProductRepository.Get(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found");

            _uow.ProductRepository.Delete(product);
            _uow.Commit();

            return Ok();
        }
    }
}
