using System;
using Core.Utilities.Results;
using Entities.Dtos.Job;

namespace Business.Abstract
{
	public interface IJobService
	{
		IResult Add(JobToAddDto jobToAdd);
		List<JobDto> GetByIds(List<int> ids);
		IDataResult<List<JobDto>> GetByEmployerId(bool showClosedOnes, int employerId);
		IDataResult<List<JobDto>> GetAll(JobFilterDto? jobFilterDto = null);
		IDataResult<List<JobDto>> GetSearchResults(JobFilterDto jobFilterDto, string searchKey);
        IDataResult<List<JobDto>> GetRelatedJobs(int jobId);
		IResult Update(JobToUpdateDto jobToUpdate, int employerId);
    }
}

