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

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var claims = _authService.Login(userForLoginDto).Data;
            var claimsIdentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            });
            return Redirect("/");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("employeeRegister")]
        public async Task<IActionResult> EmployeeRegister(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var claims = _authService.EmployeeRegister(employeeForRegisterDto).Data;
            var claimsIdentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            });
            return Redirect("/");
        }

        [HttpPost]
        [Route("employerRegister")]
        public async Task<IActionResult> EmployerRegister(EmployerForRegisterDto employerForRegisterDto)
        {
            var claims = _authService.EmployerRegister(employerForRegisterDto).Data;
            var claimsIdentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
            await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            });
            return Redirect("/");
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

