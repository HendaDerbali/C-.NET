using System.ComponentModel.DataAnnotations;
namespace DojoSurveyWithModel1.Models;
#pragma warning disable CS8618


    
public class Dojo
{

    public string Name { get; set; }

    public string Location { get; set; }

    public int Language { get; set; }
    public int Comment { get; set; }


}

