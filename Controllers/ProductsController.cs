using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Models.Request;
using WebApplication1.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProductsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _uow.ProductRepository.GetAll().ToList();

            if (products is null)
                return NotFound("Empty product list");

            return products;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<Product> Get(int id)
        {
            var product = _uow.ProductRepository.Get(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found");

            return product;
        }


        [HttpPost]
        public ActionResult Post(ProductRequest productRequest)
        {
            var product = new Product(productRequest);

            if (product is null)
                return BadRequest();

            _uow.ProductRepository.Add(product);
            _uow.Commit();

            return new CreatedAtRouteResult("GetProductById",
                new {id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            _uow.ProductRepository.Update(product);
            _uow.Commit();

            return Ok(product);
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
