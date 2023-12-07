using System;
using Core.Utilities.Results;
using Entities.Dtos.Job;

namespace Business.Abstract
{
	public interface IFavoriteService
	{
		IDataResult<List<JobDto>> GetFavorites(int employeeId);
		IResult Add(int jobId, int employeeId);
		IResult Delete(int id, int employeeId);
	}
}

