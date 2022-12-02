using Domain.ValueTypes;

namespace Domain.Applicants
{
    public class ProfileDocumentDetail
    {
        public string Id { get; set; }
        public string IdentityId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        public string DocumentText { get; set; }

        public List<TextAnalysis> Analysis { get; set; }

    }
}
