using Entities.Concrete;
using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class JobSearchModel
    {
        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
        public List<JobCategory> Categories { get; set; }
        public JobFilterDto JobFilter { get; set; }
        public List<JobDto> Jobs { get; set; }
        public string? SearchKey { get; set; }
    }
}
