using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfJobApplicationDal : EfEntityRepositoryBase<JobApplication, Context>, IJobApplicationDal
    {
    }
}

