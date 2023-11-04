using System;
using Core.Utilities.Results;
using Entities.Dtos.Job;

namespace Business.Abstract
{
	public interface IFavoriteService
	{
		IDataResult<List<JobDto>> GetFavorites();
		IResult Add(int jobId);
		IResult Delete(int id);
	}
}

