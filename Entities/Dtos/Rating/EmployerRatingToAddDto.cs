using System;
namespace Entities.Dtos.Rating
{
	public class EmployerRatingToAddDto
	{
        public int UserId { get; set; }
        public int EmployerId { get; set; }
		public int Rating { get; set; }
		public string? Comment { get; set; }
	}
}

