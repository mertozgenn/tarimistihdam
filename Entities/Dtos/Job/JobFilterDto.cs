using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Job
{
    public class JobFilterDto
    {
        public int? CategoryId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public List<string>? TagKeys { get; set; }
        public int? MinWage { get; set; }
        public int? MaxWage { get; set; }
    }
}
