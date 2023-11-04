using System;
namespace Entities.Dtos.Job
{
	public class JobDto
	{
		public int Id { get; set; }
		public string Employer { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public DateTime PublishDate { get; set; }
		public string Description { get; set; }
		public decimal DailyWage { get; set; }
		public List<string> Tags { get; set; }
		public string? EmployerProfilePhoto { get; set; }
	}
}

