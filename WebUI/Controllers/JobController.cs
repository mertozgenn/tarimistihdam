using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos.Job;
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

        public JobController(IJobCategoryService jobCategoryService, ICityDistrictService cityDistrictService,
            IJobService jobService)
        {
            _jobCategoryService=jobCategoryService;
            _cityDistrictService=cityDistrictService;
            _jobService=jobService;
        }

        [Route("is-ilanlarim")]
        public IActionResult Index()
        {
            return View();
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
                return Redirect("is-ilanlarim");
            }
            else
            {
                return returnToView(result.Message);
            }
        }

        public JsonResult GetDistricts([FromQuery] int cityId)
        {
            var districts = _cityDistrictService.GetDistrictsByCityId(cityId).Data;
            return Json(districts);
        }


        [Route("is-ilanlari")]
        public IActionResult FindJob()
        {
            return View();
        }

        [Route("is-ilani-detay")]
        public IActionResult JobDetail()
        {
            return View();
        }
    }
}

