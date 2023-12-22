using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Rating;

namespace DataAccess.Abstract
{
	public interface IEmployeeRatingDal: IEntityRepository<EmployeeRating>
	{
		bool CanRate(int employeeId, int employerId);
		List<EmployeeRatingDto> GetAllDto(int employeeId);
	}
}

