using Entities.Concrete;
using Entities.Dtos.Interest;
using Entities.Dtos.User;

namespace WebUI.Models
{
    public class EmployeeMyProfileModel
    {
        public EmployeeInformationToUpdateDto EmployeeInformation { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<InterestDto> Interests { get; set; }
        public List<JobTag> Tags { get; set; }
        public string? Message { get; set; }
    }
}
