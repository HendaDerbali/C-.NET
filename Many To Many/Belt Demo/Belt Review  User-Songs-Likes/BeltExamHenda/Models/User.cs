#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExamHenda.Models;

public class User 
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; }
    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string PassConfirm { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatesAt { get; set; } = DateTime.Now;
    
    // One To Many 
    public List<Song> CreatedSongs { get; set; } = new List<Song>(); 
    // Many To Many 
    public List<Like> SongsLiked { get; set; } = new List<Like>();


}