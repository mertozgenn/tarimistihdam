using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class JobController : Controller
    {
        private IJobCategoryService _jobCategoryService;
        private ICityDistrictService _cityDistrictService;
        private IJobService _jobService;
        private IJobApplicationService _jobApplicationService;
        private IFavoriteService _favoriteService;

        public JobController(IJobCategoryService jobCategoryService, ICityDistrictService cityDistrictService,
            IJobService jobService, IJobApplicationService jobApplicationService, IFavoriteService favoriteService)
        {
            _jobCategoryService=jobCategoryService;
            _cityDistrictService=cityDistrictService;
            _jobService=jobService;
            _jobApplicationService=jobApplicationService;
            _favoriteService=favoriteService;
        }

        [Route("is-ilanlarim")]
        public IActionResult MyJobs()
        {
            bool isEmployer = User.Claims.FirstOrDefault(x => x.Type == "EmployerId") != null;
            if (isEmployer)
            {
                var employerClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
                int employerId = int.Parse(employerClaim.Value);
                var result = _jobService.GetByEmployerId(true, employerId);
                MyJobsModel model = new MyJobsModel()
                {
                    Jobs = result.Data != null ? result.Data : new List<JobDto>(),
                    Message = result.Message
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("EmployeeAppliedJobs", "JobApplication");
            }
        }

        [Route("ilan-formu")]
        public IActionResult JobPosting()
        {
            var cities = _cityDistrictService.GetCities().Data;
            JobPostingModel model = new JobPostingModel
            {
                Categories = _jobCategoryService.GetAll().Data,
                Cities = cities,
                Districts = _cityDistrictService.GetDistrictsByCityId(cities.First().Id).Data,
                JobToAdd = new JobToAddDto()
                {
                    DailyWage = 500,
                    EmployeeCount = 50
                }
            };
            return View(model);
        }

        [Route("ilan-formu/{jobId}")]
        public IActionResult JobUpdate(int jobId)
        {
            var cities = _cityDistrictService.GetCities().Data;
            var job = _jobService.GetByIds(new List<int> { jobId }).First();
            JobUpdateModel model = new JobUpdateModel
            {
                Categories = _jobCategoryService.GetAll().Data,
                Cities = cities,
                Districts = _cityDistrictService.GetDistrictsByCityId(job.CityId).Data,
                JobToUpdate = new JobToUpdateDto
                {
                    DailyWage = job.DailyWage,
                    Description = job.Description,
                    EmployeeCount = job.EmployeeCount,
                    Id = job.Id,
                    CategoryId = job.CategoryId,
                    CityId = job.CityId,
                    DistrictId = job.DistrictId ?? 0,
                    Status = job.Status,
                    Title = job.Title,
                }
            };
            return View(model);
        }

        [HttpPost]
        [Route("ilan-formu")]
        public IActionResult JobPosting(JobToAddDto jobToAdd)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
            jobToAdd.EmployerId = int.Parse(claim.Value);
            Func<string, IActionResult> returnToView = (message) =>
            {
                JobPostingModel model = new JobPostingModel();
                model.Message = message;
                model.JobToAdd = jobToAdd;
                model.Categories = _jobCategoryService.GetAll().Data;
                model.Cities = _cityDistrictService.GetCities().Data;
                model.Districts = _cityDistrictService.GetDistrictsByCityId(model.JobToAdd.CityId).Data;
                return View(model);
            };
            if (!ModelState.IsValid)
            {
                return returnToView("Formu eksiksiz doldurunuz.");
            }
            var result = _jobService.Add(jobToAdd);
            if (result.Success)
            {
                return Redirect("/is-ilanlarim");
            }
            else
            {
                return returnToView(result.Message);
            }
        }

        [HttpPost]
        [Route("ilan-formu/{jobId}")]
        public IActionResult JobUpdate(JobToUpdateDto jobToUpdate)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
            var employerId = int.Parse(claim.Value);
            Func<string, IActionResult> returnToView = (message) =>
            {
                JobUpdateModel model = new JobUpdateModel();
                model.Message = message;
                model.JobToUpdate = jobToUpdate;
                model.Categories = _jobCategoryService.GetAll().Data;
                model.Cities = _cityDistrictService.GetCities().Data;
                model.Districts = _cityDistrictService.GetDistrictsByCityId(jobToUpdate.CityId).Data;
                return View(model);
            };
            if (!ModelState.IsValid)
            {
                return returnToView("Formu eksiksiz doldurunuz.");
            }
            var result = _jobService.Update(jobToUpdate, employerId);
            if (result.Success)
            {
                return Redirect("/is-ilanlarim");
            }
            else
            {
                return returnToView(result.Message);
            }
        }

        [Route("is-ilanlari")]
        public IActionResult FindJob([FromQuery] JobFilterDto? jobFilterDto)
        {
            var cities = _cityDistrictService.GetCities().Data;
            var categories = _jobCategoryService.GetAll().Data;
            var jobs = _jobService.GetAll(jobFilterDto).Data;
            List<District> districts = new List<District>();
            if (jobFilterDto != null && jobFilterDto.CityId != null)
            {
                districts = _cityDistrictService.GetDistrictsByCityId((int)jobFilterDto.CityId).Data;
            }
            FindJobModel model = new FindJobModel
            {
                Categories = categories,
                Cities = cities,
                JobFilter = jobFilterDto != null ? jobFilterDto : new JobFilterDto(),
                Jobs = jobs,
                Districts = districts
            };
            return View(model);
        }

        [Route("is-ilani-detay/{jobId}")]
        public IActionResult JobDetail(string jobId)
        {
            var job = _jobService.GetByIds(new int[] {int.Parse(jobId)}.ToList());
            var relatedJobs = _jobService.GetRelatedJobs(int.Parse(jobId)).Data;
            bool isApplied = true;
            bool isFavorite = true;
            var employeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            if (employeClaim != null)
            {
                var employeeId = int.Parse(employeClaim.Value);
                isApplied = _jobApplicationService.IsApplied(int.Parse(jobId), employeeId).Success;
                isFavorite = _favoriteService.IsFavorite(int.Parse(jobId), employeeId).Success;
            }
            JobDetailModel model = new JobDetailModel
            {
                Job = job.First(),
                RelatedJobs = relatedJobs,
                IsApplied = isApplied,
                IsFavorite = isFavorite
            };
            return View(model);
        }

        [Route("arama-sonuclari/{searchKey}")]
        public IActionResult JobSearch([FromQuery] JobFilterDto? jobFilterDto, string searchKey)
        {
            var cities = _cityDistrictService.GetCities().Data;
            var categories = _jobCategoryService.GetAll().Data;
            var jobs = _jobService.GetSearchResults(jobFilterDto, searchKey).Data;
            List<District> districts = new List<District>();
            if (jobFilterDto != null && jobFilterDto.CityId != null)
            {
                districts = _cityDistrictService.GetDistrictsByCityId((int)jobFilterDto.CityId).Data;
            }
            JobSearchModel model = new JobSearchModel
            {
                Categories = categories,
                Cities = cities,
                JobFilter = jobFilterDto != null ? jobFilterDto : new JobFilterDto(),
                Jobs = jobs,
                Districts = districts,
                SearchKey = searchKey
            };
            return View(model);
        }
    }
}

