using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: /<controller>/
        [Route("giris-yap")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("kayit-ol")]
        public IActionResult Signin()
        {
            return View();
        }
    }
}

