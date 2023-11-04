using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface IEmployerRatingDal: IEntityRepository<EmployerRating>
	{
		bool CanRate(int employerId, int employeeId);
	}
}

