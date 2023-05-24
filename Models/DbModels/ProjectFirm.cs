namespace AkwadratDesign.Models.DbModels
{
    /// <summary>
    /// Klasa: ProjectFirm
    /// - Klasa `ProjectFirm` reprezentuje powiązanie wiele do wielu między firmą (obiektem klasy `Firm`) a projektem (obiektem klasy `Project`).
    /// - Właściwość `FirmsId` przechowuje identyfikator firmy.
    /// - Właściwość `ProjectsId` przechowuje identyfikator projektu.
    /// - Właściwości `Firm` i `Project` są obiektami powiązanymi z tym powiązaniem.
    /// - Wartości domyślne dla właściwości `Firm` i `Project` to null!, co oznacza, że początkowo nie mają przypisanych żadnych wartości.
    /// </summary>
    public class ProjectFirm
    {
        public int FirmsId { get; set; }
        public int ProjectsId { get; set; }
        public Firm? Firm { get; set; } = null!;
        public Project? Project { get; set; } = null!;
    }
}
