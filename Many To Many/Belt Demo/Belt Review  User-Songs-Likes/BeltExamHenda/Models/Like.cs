#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExamHenda.Models;

public class Like
{
    public int LikeId { get; set; }
    public int UserId { get; set; }
    public int SongId { get; set; }
    public User? Person { get; set; }
    public Song? Magazine { get; set; }


}
