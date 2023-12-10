using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("giris-yap")]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("giris-yap")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", "Lütfen formu eksiksiz doldurunuz.");
            }
            var result = _authService.Login(userForLoginDto);
            if (result.Success)
            {
                var claimsIdentiy = new ClaimsIdentity(result.Data, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return Redirect("/");
            }
            return View("Login", result.Message);
        }

        [Route("isveren-kayit")]
        public IActionResult EmployerRegister()
        {
            EmployerRegisterModel employerRegisterModel = new EmployerRegisterModel()
            {
                EmployerForRegister = new EmployerForRegisterDto()
            };
            return View(employerRegisterModel);
        }

        [Route("is-arayan-kayit")]
        public IActionResult EmployeeRegister()
        {
            EmployeeRegisterModel employeeRegisterModel = new EmployeeRegisterModel()
            {
                EmployeeForRegister = new EmployeeForRegisterDto()
            };
            return View(employeeRegisterModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("is-arayan-kayit")]
        public async Task<IActionResult> EmployeeRegister(EmployeeForRegisterDto employeeForRegister)
        {
            if (!ModelState.IsValid)
            {
                EmployeeRegisterModel employeeRegisterModel = new EmployeeRegisterModel()
                {
                    EmployeeForRegister = employeeForRegister,
                    Message = "Lütfen formu eksiksiz doldurunuz."
                };
                return View(employeeRegisterModel);
            }
            var result = _authService.EmployeeRegister(employeeForRegister);
            if (result.Success)
            {
                var claimsIdentiy = new ClaimsIdentity(result.Data, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return Redirect("/");
            }
            else
            {
                EmployeeRegisterModel employeeRegisterModel = new EmployeeRegisterModel()
                {
                    EmployeeForRegister = employeeForRegister,
                    Message = result.Message
                };
                return View(employeeRegisterModel);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("isveren-kayit")]
        public async Task<IActionResult> EmployerRegister(EmployerForRegisterDto employerForRegister)
        {
            if (!ModelState.IsValid)
            {
                EmployerRegisterModel employerRegisterModel = new EmployerRegisterModel()
                {
                    EmployerForRegister = employerForRegister,
                    Message = "Lütfen formu eksiksiz doldurunuz."
                };
                return View(employerRegisterModel);
            }
            var result = _authService.EmployerRegister(employerForRegister);
            if (result.Success)
            {
                var claimsIdentiy = new ClaimsIdentity(result.Data, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return Redirect("/");
            }
            else
            {
                EmployerRegisterModel employerRegisterModel = new EmployerRegisterModel()
                {
                    EmployerForRegister = employerForRegister,
                    Message = result.Message
                };
                return View(employerRegisterModel);
            }
        }

        [Route("cikis")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

