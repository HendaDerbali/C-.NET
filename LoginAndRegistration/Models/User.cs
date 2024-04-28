#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAndRegistration.Models;
public class User
{
    [Key]
    [Required]
    public int UserId { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "FirstName should be at least 2 caracters")]
    public string FirstName { get; set; } 

    [Required]
    [MinLength(2, ErrorMessage = "LastName should be at least 2 caracters")]
    public string LastName { get; set; } 

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    
    [Required]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } 

    [NotMapped] // not send this to DB
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string PwdConfirm { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
   
}