using System.Diagnostics;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private ICityDistrictService _cityDistrictService;
    private IJobCategoryService _jobCategoryService;
    private IJobService _jobService;
    public HomeController(ICityDistrictService cityDistrictService, IJobCategoryService jobCategoryService, IJobService jobService)
    {
        _cityDistrictService = cityDistrictService;
        _jobCategoryService = jobCategoryService;
        _jobService=jobService;
    }

    [Route("")]
    public IActionResult Index()
    {
        var cities = _cityDistrictService.GetCities().Data;
        var jobCategories = _jobCategoryService.GetAll().Data;
        var latestJobs = _jobService.GetLatestJobs().Data;
        var model = new HomeModel
        {
            Cities = cities != null ? cities : new List<City>(),
            JobCategories = jobCategories != null ? jobCategories : new List<JobCategory>(),
            LatestJobs = latestJobs != null ? latestJobs : new List<JobDto>()
        };
        return View(model);
    }
}

