using System;
namespace Entities.Dtos.WorkExperience
{
	public class WorkExperienceToAddDto
	{
		public string Title { get; set; }
		public string? Description { get; set; }
        public int EmployeeId { get; set; }
    }
}

