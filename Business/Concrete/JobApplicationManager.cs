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
        private IEmployerService _employerService;
        private IJobService _jobService;
        private IRatingService _ratingService;

        public JobApplicationManager(IJobApplicationDal jobApplicationDal, IJobDal jobDal, 
            IEmployeeService employeeService, IJobService jobService, IRatingService ratingService,
            IEmployerService employerService)
        {
            _jobApplicationDal=jobApplicationDal;
            _jobDal=jobDal;
            _employeeService=employeeService;
            _jobService=jobService;
            _ratingService = ratingService;
            _employerService = employerService;
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

        public IDataResult<List<AppliedJobDto>> GetAppliedJobs(int employeeId)
        {
            var applications = _jobApplicationDal.GetAll(x => x.EmployeeId == employeeId);
            var jobs = _jobDal.GetAllDto(x => applications.Exists(y => y.JobId == x.Id));
            var employee = _employeeService.GetEmployeeInformation(employeeId).Data;
            List<AppliedJobDto> result = new List<AppliedJobDto>();
            foreach (var item in jobs)
            {
                result.Add(new AppliedJobDto
                {
                    Category = item.Category,
                    City = item.City,
                    CityId = item.CityId,
                    DailyWage = item.DailyWage,
                    Description = item.Description,
                    District = item.District,
                    DistrictId = item.DistrictId,
                    EmployeeCount = item.EmployeeCount,
                    CategoryId = item.CategoryId,
                    EmployerId = item.EmployerId,
                    EmployerProfilePhoto = item.EmployerProfilePhoto,
                    Employer = item.Employer,
                    Id = item.Id,
                    Image = item.Image,
                    IsActive = item.IsActive,
                    NlpTags = item.NlpTags,
                    PublishDate = item.PublishDate,
                    Status = item.Status,
                    Title = item.Title,
                    Tags = item.Tags,
                    CanRate = _ratingService.EmployeeCanRateEmployer(item.EmployerId, employee.UserId).Success,
                    IsApproved = applications.First(x => x.JobId == item.Id).IsApproved,
                });
            }
            return new SuccessDataResult<List<AppliedJobDto>>(result);
        }

        public IDataResult<List<CandidateDto>> GetCandidates(int jobId, int employerId)
        {
            var job = _jobService.GetByIds(new List<int> { jobId });
            if (job.First().EmployerId != employerId)
            {
                return new ErrorDataResult<List<CandidateDto>>(Messages.AccessDenied);
            }
            var employer = _employerService.GetEmployerInformation(employerId).Data;
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
                    CanRate = _ratingService.EmployerCanRateEmployee(employee.EmployeeId, employer.UserId).Success,
                    IsJobActive = job.First().IsActive,
                    Rating = employee.Rating,
                    RatingCount = employee.RatingCount
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

