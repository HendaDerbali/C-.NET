#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExamHenda.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public int WeddingId { get; set; }
    public User? User { get; set; }
    public Wedding? Wedding { get; set; }
}
