using System;
namespace Entities.Dtos.Rating
{
	public class EmployeeRatingToAddDto
	{
		public int EmployeeId { get; set; }
		public int Rating { get; set; }
		public string? Comment { get; set; }
	}
}

