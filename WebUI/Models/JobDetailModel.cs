using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class JobDetailModel
    {
        public JobDto Job { get; set; }
        public List<JobDto> RelatedJobs { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsApplied { get; set; }
    }
}
