using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class JobController : Controller
    {
        [Route("is-ilanlarim")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("ilan-formu")]
        public IActionResult JobPosting()
        {
            return View();
        }
    }
}

