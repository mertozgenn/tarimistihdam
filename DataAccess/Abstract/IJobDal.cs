using System;
using System.Linq.Expressions;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Abstract
{
	public interface IJobDal: IEntityRepository<Job>
	{
		List<JobDto> GetAllDto(Predicate<JobDto>? filter = null, JobFilterDto? jobFilterDto = null);
	}
}

