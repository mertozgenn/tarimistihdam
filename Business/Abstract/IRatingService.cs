using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.Rating;

namespace Business.Abstract
{
	public interface IRatingService
	{
		IResult AddEmployeeRating(EmployeeRatingToAddDto employeeRatingToAddDto);
		IResult DeleteEmployeeRating(int id, int userId);
		IResult AddEmployerRating(EmployerRatingToAddDto employerRatingToAddDto);
		IResult DeleteEmployerRating(int id, int userId);
		IResult EmployerCanRateEmployee(int employeeId, int employerUserId);
		IResult EmployeeCanRateEmployer(int employerId, int employeeUserId);
		IDataResult<List<EmployeeRatingDto>> GetEmployeeRatings(int employeeId);
		IDataResult<List<EmployerRatingDto>> GetEmployerRatings(int employerId);
	}
}

