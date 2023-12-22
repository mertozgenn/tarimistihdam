using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class MyJobsModel
    {
        public List<JobDto> Jobs { get; set; }
        public string Message { get; set; }
    }
}
