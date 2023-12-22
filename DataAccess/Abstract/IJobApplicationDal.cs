using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Abstract
{
	public interface IJobApplicationDal: IEntityRepository<JobApplication>
	{
	}
}

