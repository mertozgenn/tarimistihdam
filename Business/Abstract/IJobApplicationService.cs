using Core.Utilities.Results;
using Entities.Dtos.Job;
using Entities.Dtos.User;
using System;
namespace Business.Abstract
{
	public interface IJobApplicationService
	{
		IResult Apply(int jobId, int employeeId);
		IResult IsApplied(int jobId, int employeeId);
		IDataResult<List<JobDto>> GetAppliedJobs(int employeeId);
		IDataResult<List<CandidateDto>> GetCandidates(int jobId, int employerId);
		IResult ApproveApplication(int jobId, int employeeId, int employerId);
	}
}

