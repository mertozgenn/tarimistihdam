using System;
using System.Linq.Expressions;
using Business.Abstract;
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

		public JobManager(IJobDal jobDal, IJobTagService jobTagService)
		{
            _jobDal = jobDal;
            _jobTagService = jobTagService;
		}

        public IResult Add(JobToAddDto jobToAdd)
        {
            string fileName = "/images/jobs/default.jpg";
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
                Status = "Active",
            };
            _jobDal.Add(job);
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
            var data = _jobDal.GetAllDto(jobFilterDto: jobFilterDto);
            return new SuccessDataResult<List<JobDto>>(data);
        }

        public IDataResult<List<JobDto>> GetByEmployerId(int employerId)
        {
            var data = _jobDal.GetAllDto(x => x.EmployerId == employerId);
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
            var job = _jobDal.Get(x => x.Id == jobId);
            if (job == null)
            {
                return new ErrorDataResult<List<JobDto>>("İş ilanı bulunamadı");
            }
            var data = _jobDal.GetAllDto(x => 
            (x.CategoryId == job.CategoryId || x.NlpTags.Contains(job.NlpTags)) && 
            x.Id != jobId).Take(4).ToList();
            return new SuccessDataResult<List<JobDto>>(data);
        }
    }
}

