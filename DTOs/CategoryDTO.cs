using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public ICollection<ProductDTO>? products { get; set; }
    }
}
