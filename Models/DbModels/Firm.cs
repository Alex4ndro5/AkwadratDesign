using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkwadratDesign.Models.DbModels
{
    /// <summary>
    ///   Klasa: Firm
    ///   Właściwości:
    ///   - FirmId (int): Główny identyfikator firmy.
    ///   - FirmName (string): Nazwa firmy. Ta właściwość jest wymagana. Pobiera lub ustawia nazwę firmy. Minimalna liczba znaków to 2, a maksymalna 50.
    ///   - ProjectFirms (List<ProjectFirm>): Lista firm projektowych powiązanych z tą firmą. (połączenie wiele do wielu)
    ///   - Projects (List<Project>): Lista projektów powiązanych z tą firmą. (połączenie wiele do wielu)
    /// </summary>
    public class Firm
    {
        [Key]
        public int FirmId { get; set; }
        [Display(Name = "Nazwa Firmy")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirmName { get; set; }
        public List<ProjectFirm> ProjectFirms { get; } = new();
        public List<Project> Projects { get; } = new();
    }
}
