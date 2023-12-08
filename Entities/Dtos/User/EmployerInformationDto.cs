using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class EmployerInformationDto: UserInformationDto
    {
        public string TaxPlace { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyName { get; set; }
    }
}
