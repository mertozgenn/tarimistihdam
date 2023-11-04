using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Job;

namespace Business.Concrete
{
	public class JobManager: IJobService
	{
        private IJobDal _jobDal;

		public JobManager(IJobDal jobDal)
		{
            _jobDal = jobDal;
		}

        public IDataResult<List<JobDto>> GetAll()
        {
            var data = _jobDal.GetAllDto();
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public List<JobDto> GetByIds(List<int> ids)
        {
            var data = _jobDal.GetAllDto(x => ids.Contains(x.Id));
            return data;
        }
    }
}

