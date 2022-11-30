using Domain.ValueTypes;

namespace Application.Applicant.Dto
{
    public class ApplicantProfileDocumentDetail
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public List<TextAnalysis> Analysis { get; set; }
    }
}
