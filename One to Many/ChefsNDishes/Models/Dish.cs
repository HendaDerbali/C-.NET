#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsNDishes;
public class Dish
{
    [Key]
    [Required]
    public int DishId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(5)]
    public string Name { get; set; } 

    [Required(ErrorMessage = "Tastiness is required")]
    [Range(1, 5, ErrorMessage = "Tastiness must between 1 &5")]
    public int Tastiness { get; set; }

    [Required(ErrorMessage = "Calories is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Colories muset be grather than 0")]
    public int  Calories { get; set; }
 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Required]
	public int ChefId { get; set; }
	public Chef? Creator { get; set; }  
}
