using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Job
{
    public class JobToAddDto
    {
        public int EmployerId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal DailyWage { get; set; }
        public int EmployeeCount { get; set; }
        public IFormFile? Image { get; set; }
    }
}
