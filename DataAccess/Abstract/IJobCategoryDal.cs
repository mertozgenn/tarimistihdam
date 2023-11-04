using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface IJobCategoryDal: IEntityRepository<JobCategory>
	{
	}
}

