using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfJobDal: EfEntityRepositoryBase<Job, Context>, IJobDal
	{
	}
}

