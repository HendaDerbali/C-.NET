#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExamHenda.Models;


public class Song
{
    [Key]
    public int SongId { get; set; }
    [Required]
    [MinLength(2)]
    public string Title { get; set; }
    [Required]
    [Range(0,59)]
    public int DurMinutes{ get; set; }
    [Required]
    [Range(0,59)]
    public int DurSeconds{ get; set; }
    [Required]
    public string Genre { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatesAt { get; set; } = DateTime.Now;
    
    //One to Many :
    public int UserId { get; set; }
    // Navigation property for related User object
    public User? Creator { get; set; }

    // Many To Many :
    public List<Like> UsersWhoLike { get; set; } = new List<Like>();

}