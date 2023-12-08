using System;
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
                fileName = FileHelper.CreateFile(jobToAdd.Image, "/images/jobs", guid);
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

        public IDataResult<List<JobDto>> GetAll()
        {
            var data = _jobDal.GetAllDto();
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
    }
}

