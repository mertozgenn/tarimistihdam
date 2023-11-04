using System;
using Core.Utilities.Results;
using Entities.Dtos.Job;

namespace Business.Abstract
{
	public interface IJobService
	{
		List<JobDto> GetByIds(List<int> ids);
		IDataResult<List<JobDto>> GetAll();
	}
}

