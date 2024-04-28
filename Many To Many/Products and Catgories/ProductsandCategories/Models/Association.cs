#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsandCategories.Models;


public class Association
{
    [Key]
    [Required]
    public int AssociationId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Product? Product { get; set; }
    public Category? Category { get; set; }
    

}  
