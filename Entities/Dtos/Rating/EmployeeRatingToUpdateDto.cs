using System;
namespace Entities.Dtos.Rating
{
	public class EmployeeRatingToUpdateDto
	{
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}

