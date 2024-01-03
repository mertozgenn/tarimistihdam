using Entities.Concrete;
using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class HomeModel
    {
        public List<City> Cities { get; set; }
        public List<JobCategory> JobCategories { get; set; }
        public List<JobDto> LatestJobs { get; set; }
        public List<JobDto> SuggestedJobs { get; set; }
    }
}
