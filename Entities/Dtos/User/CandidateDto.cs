using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class CandidateDto: EmployeeInformationDto
    {
        public bool IsApproved { get; set; }
        public bool CanRate { get; set; }
        public bool IsJobActive { get; set; }
    }
}
