using System;
using Core.Utilities.Results;
using Entities.Dtos.Rating;

namespace Business.Abstract
{
	public interface IRatingService
	{
		IResult AddEmployeeRating(EmployeeRatingToAddDto employeeRatingToAddDto);
		IResult UpdateEmployeeRating(EmployeeRatingToUpdateDto employeeRatingToUpdateDto);
		IResult DeleteEmployeeRating(int id);
		IResult AddEmployerRating(EmployerRatingToAddDto employerRatingToAddDto);
		IResult UpdateEmployerRating(EmployerRatingToUpdateDto employerRatingToUpdateDto);
		IResult DeleteEmployerRating(int id);
	}
}

