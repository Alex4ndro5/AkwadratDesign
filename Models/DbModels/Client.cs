using System.ComponentModel.DataAnnotations;

namespace AkwadratDesign.Models.DbModels
{
    /// <summary>
    /// Klasa: Client 
    /// - Właściwość `ClientId` służy jako unikalny identyfikator dla klienta.
    /// - Właściwość `Name` przechowuje imię klienta.
    /// - Właściwość `Surname` przechowuje nazwisko klienta.
    /// - Właściwość `Email` przechowuje adres e-mail klienta.
    /// - Właściwość `Message` przechowuje wiadomość od klienta.
    /// - Właściwości `Name`, `Surname`, `Email` i `Message` są oznaczone jako `Required`, co oznacza, że muszą mieć wartość.
    /// </summary>
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Display(Name = "Imie")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();

    }
}
