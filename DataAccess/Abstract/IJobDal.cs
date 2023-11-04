using System;
using System.Linq.Expressions;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Abstract
{
	public interface IJobDal: IEntityRepository<Job>
	{
		List<JobDto> GetAllDto(Expression<Func<JobDto, bool>>? filter = null);
	}
}

