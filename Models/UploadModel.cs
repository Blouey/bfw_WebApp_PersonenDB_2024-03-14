using System.ComponentModel.DataAnnotations;

namespace bfw_WebApp_PersonenDB_2024_03_14.Models;

public class UploadModel
{
    [Required]
    public Person PersonModel { get; set; }
    
    public IFormFile? Uploaddatei { get; set; }
}