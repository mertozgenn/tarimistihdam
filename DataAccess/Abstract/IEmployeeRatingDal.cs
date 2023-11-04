using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface IEmployeeRatingDal: IEntityRepository<EmployeeRating>
	{
	}
}

