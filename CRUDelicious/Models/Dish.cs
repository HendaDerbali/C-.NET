#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    [Required]
    public int DishId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [MinLength(5)]
    public string Name { get; set; } 
    [Required(ErrorMessage = "Chef is required")]
    public string Chef { get; set; }
    [Required(ErrorMessage = "Tastiness is required")]
    [Range(1, 5, ErrorMessage = "Tastiness must between 1 &5")]
    public int Tastiness { get; set; }

    [Required(ErrorMessage = "Calories is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Colories muset be >0")]
    public int  Calories { get; set; }
    [Required (ErrorMessage = "TEXT is required")]
    public string  TEXT { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
