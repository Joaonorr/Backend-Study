using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }


}
