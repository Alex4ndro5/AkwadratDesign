using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkwadratDesign.Models.DbModels
{
    /// <summary>
    ///   Klasa: Firm
    ///   Właściwości:
    ///   - FirmId (int): Główny identyfikator firmy.
    ///   - FirmName (string): Nazwa firmy. Ta właściwość jest wymagana.
    ///   - ClientId (int): Klucz obcy odnoszący się do powiązanego klienta.
    ///   - Client (Client): Obiekt klienta powiązany z firmą
    ///   - ProjectFirms (List<ProjectFirm>): Lista firm projektowych powiązanych z tą firmą. (połączenie wiele do wielu)
    ///   - Projects (List<Project>): Lista projektów powiązanych z tą firmą. (połączenie wiele do wielu)
    /// </summary>
    public class Firm
    {
        [Key]  
        public int FirmId { get; set; }
        [Required]
        public string FirmName { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public List<ProjectFirm> ProjectFirms { get; } = new();
        public List<Project> Projects { get;} = new(); 
    }
}
