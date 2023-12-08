using System;
using Core.Utilities.Results;
using Entities.Dtos.Job;

namespace Business.Abstract
{
	public interface IJobService
	{
		IResult Add(JobToAddDto jobToAdd);
		List<JobDto> GetByIds(List<int> ids);
		IDataResult<List<JobDto>> GetByEmployerId(int employerId);
		IDataResult<List<JobDto>> GetAll();
	}
}

