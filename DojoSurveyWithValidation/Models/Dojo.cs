using System.ComponentModel.DataAnnotations;
namespace DojoSurveyWithValidation.Models;
#pragma warning disable CS8618


public class Dojo
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; }

    [Required(ErrorMessage = "Language is required")]
    public string Language { get; set; }


    [MinLength(20, ErrorMessage = "Comment should be more than 20 characters")]
    public string? Comment { get; set; }


}

