using AkwadratDesign.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AkwadratDesign.Models.DbModels
{/// <summary>
///  Klasa: Project
///  - Właściwość `ProjectId` służy jako unikalny identyfikator dla projektu.
///  - Właściwość `Title` przechowuje tytuł projektu.
///  - Właściwość `Description` przechowuje opis projektu.
///  - Właściwość `Image` przechowuje ścieżkę do obrazka projektu.
///  - Właściwość `Type` określa typ projektu.
///  - Właściwość `TypeClient` określa typ klienta projektu.
///   - Listy `ProjectFirms` i `Firms` przechowują powiązane obiekty firm projektowych i firm odpowiednio. (wynika z właściwości wiele do wielu) 
/// </summary>
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Image { get; set; }
        public TypeProject TypeProject { get; set; }
        public TypeClient TypeClient { get; set; }
        public int ClientId { get; set; } // Foreign key property
        public virtual Client Client { get; set; } // Navigation property to the associated client
        public List<ProjectFirm> ProjectFirms { get; } = new();
        public List<Firm> Firms { get; } = new();

    }
}
