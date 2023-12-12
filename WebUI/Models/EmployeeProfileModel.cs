using Entities.Concrete;
using Entities.Dtos.Interest;
using Entities.Dtos.User;

namespace WebUI.Models
{
    public class EmployeeProfileModel
    {
        public EmployeeInformationDto EmployeeInformation { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<InterestDto> Interests { get; set; }
        public string? Message { get; set; }
    }
}
