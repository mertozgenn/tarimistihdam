using Business.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class CorporateController: Controller
    {
        [Route("hakkimizda")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("iletisim")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string message, string company, string phone)
        {
            return Ok();
        }

        [Route("kvkk")]
        public IActionResult Pdpl()
        {
            return View();
        }
        [Route("gizlilik-politikamiz")]

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
