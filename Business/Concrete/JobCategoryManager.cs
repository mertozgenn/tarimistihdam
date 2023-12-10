using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
	public class JobCategoryManager: IJobCategoryService
	{
        private IJobCategoryDal _jobCategoryDal;

		public JobCategoryManager(IJobCategoryDal jobCategoryDal)
		{
            _jobCategoryDal = jobCategoryDal;
		}

        public IDataResult<List<JobCategory>> GetAll()
        {
            var result = _jobCategoryDal.GetAll().OrderBy(x => x.Name).ToList();
            return new SuccessDataResult<List<JobCategory>>(result);
        }
    }
}

