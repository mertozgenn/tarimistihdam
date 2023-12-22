using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class EmployeeInformationDto: UserInformationDto
    {
        public int EmployeeId { get; set; }
        public List<JobTag> Interests { get; set; }
    }
}
