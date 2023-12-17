using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class JobApplicationManager : IJobApplicationService
    {
        private IJobApplicationDal _jobApplicationDal;

        public JobApplicationManager(IJobApplicationDal jobApplicationDal)
        {
            _jobApplicationDal=jobApplicationDal;
        }

        public IResult Apply(int jobId, int employeeId)
        {
            JobApplication jobApplication = new JobApplication()
            {
                JobId = jobId,
                EmployeeId = employeeId,
                ApplicationDate = DateTime.Now
            };
            _jobApplicationDal.Add(jobApplication);
            return new SuccessResult(Messages.ApplicationSuccessful);
        }

        public IResult IsApplied(int jobId, int employeeId)
        {
            var result = _jobApplicationDal.Get(x => x.JobId == jobId && x.EmployeeId == employeeId);
            if (result == null)
            {
                return new ErrorResult(Messages.ApplicationNotFound);
            }
            return new SuccessResult();
        }
    }
}

