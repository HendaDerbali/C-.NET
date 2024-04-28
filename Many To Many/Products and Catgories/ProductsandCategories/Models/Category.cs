#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsandCategories.Models;

public class Category
{
    [Key]
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    // navigation properties
    public List<Association> MyProducts { get; set; } = new List<Association>();

}


