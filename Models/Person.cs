using System.ComponentModel.DataAnnotations;


namespace bfw_WebApp_PersonenDB_2024_03_14.Models;

public class Person
{
    public int Pid { get; set; }

    [Required(ErrorMessage = "Vorname ist ein Pflichtfeld.")]
    [StringLength(100, MinimumLength = 3)]
    public string Vorname { get; set; }

    [Required(ErrorMessage = "Nachname ist ein Pflichtfeld.")]
    [StringLength(100, MinimumLength = 3)]
    public string Nachname { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "unbekannt",DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
    public string? Geburtsdatum { get; set; }

    [Range(1, 300)]
    [Display(Name = "Größe")]
    [DisplayFormat(DataFormatString = "{0} cm")]
    public int? Groesse { get; set; }
    
    [DataType(DataType.Upload)]
    public string? Bild { get; set; }
    
    [Range(1, 300)]
    [Display(Name = "Gewicht")]
    [DisplayFormat(DataFormatString = "{0} kg")]
    public double? Gewicht { get; set; }

    public string GetAge()
    {
        string geburtsdatum = Geburtsdatum ?? "";

        if (DateTime.TryParse(geburtsdatum, out DateTime dt))
        {
            if (dt.Month > DateTime.Now.Month ||
                (dt.Month == DateTime.Now.Month && dt.Day > DateTime.Now.Day))
            {
                return DateTime.Now.Year - dt.Year - 1 + " Jahre";
            }

            return DateTime.Now.Year - dt.Year + " Jahre";
        }

        return "unbekannt";
    }
}