using System.Text.Json;

namespace ContosoCrafts.WebSite.Models
{
    public class WorkExperienceModel
    {
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public string Employer { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RoleDescription { get; set; }

        public override string ToString() => JsonSerializer.Serialize<WorkExperienceModel>(this);
    }
}
