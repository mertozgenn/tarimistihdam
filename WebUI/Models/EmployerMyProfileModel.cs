using Entities.Concrete;
using Entities.Dtos.User;

namespace WebUI.Models
{
    public class EmployerMyProfileModel
    {
        public EmployerInformationToUpdateDto EmployerInformation { get; set; }
        public string? Message { get; set; }
    }
}
