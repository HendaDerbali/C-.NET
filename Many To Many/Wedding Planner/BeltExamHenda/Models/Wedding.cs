#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExamHenda.Models;

public class Wedding 
{
    [Key]
    public int WeddingId { get; set; }
    [Required]
    public string WedderOne { get; set; }
    [Required]
    public string WedderTwo { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
    public DateTime Date { get; set; } 

    [Required]
    public string Address { get; set; }
    // One To Many :
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatesAt { get; set; } = DateTime.Now;

    // Navigation property for related Message objects.
    //One To many
    public User? Creator { get; set; }

     // Many To Many
    public List<Reservation> UsersWhoReserved { get; set; } = new List<Reservation>(); 


    }

    

