using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(150)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(350)]
    public string? ImageUrl { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 1000, ErrorMessage = "Price must be between {1} and {2}")]
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
}
