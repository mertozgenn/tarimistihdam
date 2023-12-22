using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;
using Entities.Dtos.User;

namespace Business.Concrete
{
    public class JobApplicationManager : IJobApplicationService
    {
        private IJobApplicationDal _jobApplicationDal;
        private IJobDal _jobDal;
        private IEmployeeService _employeeService;
        private IJobService _jobService;
        private IRatingService _ratingService;

        public JobApplicationManager(IJobApplicationDal jobApplicationDal, IJobDal jobDal, 
            IEmployeeService employeeService, IJobService jobService, IRatingService ratingService)
        {
            _jobApplicationDal=jobApplicationDal;
            _jobDal=jobDal;
            _employeeService=employeeService;
            _jobService=jobService;
            _ratingService = ratingService;
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

        public IResult ApproveApplication(int jobId, int employeeId, int employerId)
        {
            var job = _jobService.GetByIds(new List<int> { jobId });
            if (job.First().EmployerId != employerId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            var result = _jobApplicationDal.Get(x => x.JobId == jobId && x.EmployeeId == employeeId);
            if (result == null)
            {
                return new ErrorResult(Messages.ApplicationNotFound);
            }
            result.IsApproved = true;
            _jobApplicationDal.Update(result);
            return new SuccessResult(Messages.ApplicationApproved);
        }

        public IDataResult<List<JobDto>> GetAppliedJobs(int employeeId)
        {
            var applications = _jobApplicationDal.GetAll(x => x.EmployeeId == employeeId);
            var jobs = _jobDal.GetAllDto(x => applications.Exists(y => y.JobId == x.Id));
            return new SuccessDataResult<List<JobDto>>(jobs);
        }

        public IDataResult<List<CandidateDto>> GetCandidates(int jobId, int employerId)
        {
            var job = _jobService.GetByIds(new List<int> { jobId });
            if (job.First().EmployerId != employerId)
            {
                return new ErrorDataResult<List<CandidateDto>>(Messages.AccessDenied);
            }
            var applications = _jobApplicationDal.GetAll(x => x.JobId == jobId);
            List<CandidateDto> candidates = new List<CandidateDto>();
            foreach (var application in applications)
            {
                var employee = _employeeService.GetEmployeeInformation(application.EmployeeId).Data;
                candidates.Add(new CandidateDto
                {
                    Email = employee.Email,
                    EmployeeId = employee.EmployeeId,
                    Interests = employee.Interests,
                    Name = employee.Name,
                    IsApproved = application.IsApproved,
                    Phone = employee.Phone,
                    ProfilePhoto = employee.ProfilePhoto,
                    Surname = employee.Surname,
                    Tckn = employee.Tckn,
                    UserId = employee.UserId,
                    CanRate = _ratingService.EmployerCanRateEmployee(employee.UserId, employerId).Success,
                    IsJobActive = job.First().IsActive
                });
            }
            return new SuccessDataResult<List<CandidateDto>>(candidates);
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

