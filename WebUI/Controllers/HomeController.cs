using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {

    }

    public IActionResult Index()
    {
        return View();
    }
}

