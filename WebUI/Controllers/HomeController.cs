using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    [Route("")]

    public IActionResult Index()
    {
        return View();
    }
}

