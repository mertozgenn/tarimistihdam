using System;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
	public class JobTagManager: IJobTagService
	{
		private IJobTagDal _jobTagDal;

        public JobTagManager(IJobTagDal jobTagDal)
        {
            _jobTagDal=jobTagDal;
        }

        public List<JobTag> GetJobTags()
        {
            var data = _jobTagDal.GetAll();
            return data;
        }

        public List<JobTag> GetJobTagsByIds(List<int> ids)
        {
            var data = _jobTagDal.GetAll(x => ids.Contains(x.Id));
            return data;
        }
    }
}

