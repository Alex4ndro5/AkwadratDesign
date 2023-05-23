using AkwadratDesign.Data.Enum;

namespace AkwadratDesign.ViewModels
{
    public class CreateProjectViewModel
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public TypeProject TypeProject { get; set; }
        public TypeClient TypeClient { get; set; }
    }
}
