using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class AppliedJobsModel
    {
        public List<AppliedJobDto> Jobs { get; set; }
        public string Message { get; set; }
    }
}
