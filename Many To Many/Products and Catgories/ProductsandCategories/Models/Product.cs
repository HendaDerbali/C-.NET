#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsandCategories.Models;

public class Product
{
    [Key]
    [Required]
    public int ProductId { get; set; }
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
    [Required]
    [MinLength(3)]
    public string Description { get; set; }
    [Required]
    public decimal Price{ get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    // navigation properties
    public List<Association> MyCategories { get; set; } = new List<Association>();

}
