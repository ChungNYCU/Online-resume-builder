using System.Text.Json;
/// <summary>
/// work experience control path
/// allow developer to add or remove the options in resumes
/// </summary>
namespace ContosoCrafts.WebSite.Models
{
    // getting data from json
    // allow developer to create extra options for the working experience
    public class WorkExperienceModel
    {
        // list of working experience input from user
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public string Employer { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RoleDescription { get; set; }

        // renew the user's input or get the data from the json
        public override string ToString() => JsonSerializer.Serialize<WorkExperienceModel>(this);
    }
}
