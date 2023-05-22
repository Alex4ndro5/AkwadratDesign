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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TypeProject Type { get; set; }
        public TypeClient TypeClient { get; set; }

        public List<ProjectFirm> ProjectFirms { get; } = new();
        public List<Firm> Firms { get; } = new();

        public Project()
        {
            
        }

        public Project(string title, string description, string image, TypeProject type, TypeClient typeClient)
        {
            Title = title;
            Description = description;
            Image = image;
            Type = type;
            TypeClient = typeClient;
        }
    }
}
