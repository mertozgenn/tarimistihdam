using Entities.Concrete;
using System;
namespace Entities.Dtos.Job
{
	public class JobDto
	{
		public int Id { get; set; }
        public int EmployerId { get; set; }
        public string Employer { get; set; }
        public int EmployeeCount { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string District { get; set; }
        public int? DistrictId { get; set; }
        public string Title { get; set; }
		public string Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime PublishDate { get; set; }
		public string Description { get; set; }
		public decimal DailyWage { get; set; }
		public List<JobTag> Tags { get; set; }
        public string NlpTags { get; set; }
        public string? EmployerProfilePhoto { get; set; }
        public string Image { get; set; }
    }
}

