using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Rating;

namespace DataAccess.Abstract
{
	public interface IEmployerRatingDal: IEntityRepository<EmployerRating>
	{
		bool CanRate(int employerId, int employeeId);
		List<EmployerRatingDto> GetAllDto(int employerId);
	}
}

