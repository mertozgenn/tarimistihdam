using System;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
	public interface IJobCategoryService
	{
		IDataResult<List<JobCategory>> GetAll();
	}
}

