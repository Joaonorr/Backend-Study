using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication1.Models.Request;

namespace WebApplication1.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(350)]
    public string? ImageUrl { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    public float Stock { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime EditDate { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    public Product()
    {
        var date = DateTime.Now;
        CreationDate = date;
        EditDate = date;
    }

    public Product(ProductRequest productRequest) : this()
    {
        this.Name = productRequest.Name;
        this.Description = productRequest.Description;
        this.ImageUrl = productRequest.ImageUrl;
        this.Price = productRequest.Price;
        this.Stock = productRequest.Stock;
        this.CategoryId = productRequest.CategoryId;
    }

}
