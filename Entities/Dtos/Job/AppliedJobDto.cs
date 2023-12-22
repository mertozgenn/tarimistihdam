using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Job
{
    public class AppliedJobDto: JobDto
    {
        public bool CanRate { get; set; }
        public bool IsApproved { get; set; }
    }
}
