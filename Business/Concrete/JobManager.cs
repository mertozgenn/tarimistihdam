using System;
using System.Linq.Expressions;
using Business.Abstract;
using Business.Constants;
using Business.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;
using Newtonsoft.Json;
using RestSharp;

namespace Business.Concrete
{
	public class JobManager: IJobService
	{
        private IJobDal _jobDal;
        private IJobTagService _jobTagService;
        private IInterestService _interestService;

		public JobManager(IJobDal jobDal, IJobTagService jobTagService, IInterestService interestService)
		{
            _jobDal = jobDal;
            _jobTagService = jobTagService;
            _interestService = interestService;
		}

        public IResult Add(JobToAddDto jobToAdd)
        {
            string fileName = "/assets/imgs/jobs/default.jpg";
            if (jobToAdd.Image != null)
            {
                string guid = Guid.NewGuid().ToString();
                fileName = FileHelper.CreateFile(jobToAdd.Image, "/uploads/images/jobs", guid);
            }
            Job job = new Job
            {
                CategoryId = jobToAdd.CategoryId,
                CityId = jobToAdd.CityId,
                DailyWage = jobToAdd.DailyWage,
                Description = jobToAdd.Description,
                DistrictId = jobToAdd.DistrictId,
                EmployerId = jobToAdd.EmployerId,
                EmployeeCount = jobToAdd.EmployeeCount,
                PublishDate = DateTime.Now,
                Title = jobToAdd.Title,
                Image = fileName,
                NlpTags = GetNlpTags(jobToAdd.Title + " " + jobToAdd.Description),
                IsActive = true,
                Status = "Aktif",
            };
            _jobDal.Add(job);
            return new SuccessResult();
        }

        public IResult Update(JobToUpdateDto jobToUpdate, int employerId)
        {
            var job = _jobDal.Get(x => x.Id == jobToUpdate.Id);
            if (job == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (job.EmployerId != employerId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            if (jobToUpdate.Image != null)
            {
                if (job.Image != null)
                    FileHelper.DeleteFile(job.Image);
                string guid = Guid.NewGuid().ToString();
                job.Image = FileHelper.CreateFile(jobToUpdate.Image, "/uploads/images/jobs", guid);
            }
            job.CategoryId = jobToUpdate.CategoryId;
            job.CityId = jobToUpdate.CityId;
            job.DailyWage = jobToUpdate.DailyWage;
            job.Description = jobToUpdate.Description;
            job.DistrictId = jobToUpdate.DistrictId;
            job.EmployeeCount = jobToUpdate.EmployeeCount;
            job.Title = jobToUpdate.Title;
            job.NlpTags = GetNlpTags(jobToUpdate.Title + " " + jobToUpdate.Description);
            job.Status = jobToUpdate.Status;
            if (job.Status == "Kapatıldı")
            {
                job.IsActive = false;
            }
            else if (job.Status == "Aktif")
            {
                job.IsActive = true;
            }
            _jobDal.Update(job);
            return new SuccessResult();
        }

        private string GetNlpTags(string description)
        {
            var client = new RestClient();
            var request = new RestRequest("http://185.12.109.19:8001/process-description", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { 
                description = description,
                tagPool = _jobTagService.GetJobTags().Select(x => x.Key).ToList()
            });
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<DataResult<List<string>>>(response.Content);
                return result.Data != null ? string.Join(",", result.Data.Distinct()) : "";
            }
            return "";
        }

        public IDataResult<List<JobDto>> GetAll(JobFilterDto? jobFilterDto = null)
        {
            var data = _jobDal.GetAllDto(x => x.IsActive, jobFilterDto: jobFilterDto);
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public IDataResult<List<JobDto>> GetByEmployerId(bool showClosedOnes, int employerId)
        {
            var data = _jobDal.GetAllDto(x => x.EmployerId == employerId &&
                                         (!showClosedOnes ? x.IsActive : true));
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public List<JobDto> GetByIds(List<int> ids)
        {
            var data = _jobDal.GetAllDto(x => ids.Contains(x.Id));
            return data;
        }

        public IDataResult<List<JobDto>> GetSearchResults(JobFilterDto jobFilterDto, string searchKey)
        {
            if (searchKey == null)
            {
                return new SuccessDataResult<List<JobDto>>(_jobDal.GetAllDto(jobFilterDto:jobFilterDto));
            }
            searchKey = searchKey.ToLower().Trim();
            Predicate<JobDto> filter = (x =>
            x.IsActive &&
            (x.Title ?? "").ToLower().Contains(searchKey) ||
            (x.Description ?? "").ToLower().Contains(searchKey) ||
            (x.Tags != null && x.Tags.Any(x => (x.DisplayName ?? "").ToLower().Contains(searchKey))) ||
            (x.Category ?? "").ToLower().Contains(searchKey) ||
            (x.City ?? "").ToLower().Contains(searchKey) ||
            (x.District ?? "").ToLower().Contains(searchKey) ||
            (x.Employer ?? "").ToLower().Contains(searchKey));
            var data = _jobDal.GetAllDto(filter, jobFilterDto);
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public IDataResult<List<JobDto>> GetRelatedJobs(int jobId)
        {
            var job = _jobDal.GetAllDto(x => x.Id == jobId).First();
            if (job == null)
            {
                return new ErrorDataResult<List<JobDto>>("İş ilanı bulunamadı");
            }
            var data = _jobDal.GetAllDto(x => 
            (x.CategoryId == job.CategoryId || 
            x.Tags.Any(x => job.Tags.Any(y => y.Key == x.Key))
            ) && 
            x.Id != jobId && x.IsActive).Take(4).ToList();
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public IDataResult<List<JobDto>> GetLatestJobs()
        {
            var data = _jobDal.GetAllDto(x => x.IsActive).Take(8).ToList();
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public IDataResult<List<JobDto>> GetSuggestedJobs(int employeeId)
        {
            var employeeInterests = _interestService.GetByEmployeeId(employeeId);
            if (employeeInterests == null || employeeInterests.Data == null || employeeInterests.Data.Count == 0)
            {
                return new ErrorDataResult<List<JobDto>>(new List<JobDto>());
            }
            var data = _jobDal.GetAllDto(x => x.IsActive && x.Tags != null && x.Tags.Count > 0 && 
            x.Tags.Any(x => employeeInterests.Data.Any(y => y.Name == x.DisplayName))).Take(8).ToList();
            return new SuccessDataResult<List<JobDto>>(data);
        }
    }
}

