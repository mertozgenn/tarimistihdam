using Entities.Concrete;
using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class JobUpdateModel
    {
        public JobToUpdateDto JobToUpdate { get; set; }
        public List<JobCategory> Categories { get; set; }
        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
        public string Message { get; set; }
    }
}
