using Business.Abstract;
using Entities.Dtos.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService=favoriteService;
        }

        public IActionResult AddToFavorites(int jobId)
        {
            var employeeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            if (employeeClaim == null)
            {
                return Redirect("/giris-yap");
            }
            var employeeId = int.Parse(employeeClaim.Value);
            var result = _favoriteService.Add(jobId, employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("favorilerim")]
        [Authorize(Roles = "Employee")]
        public IActionResult MyFavorites()
        {
            var employeeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            var employeeId = int.Parse(employeeClaim.Value);
            var favorites = _favoriteService.GetFavorites(employeeId);
            var model = new MyFavoritesModel
            {
                Favorites = favorites.Data ?? new List<JobDto>(),
                Message = favorites.Message
            };
            return View(model);
        }

        public IActionResult DeleteFromFavorites(int jobId)
        {
            var employeeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            var employeeId = int.Parse(employeeClaim.Value);
            var result = _favoriteService.Delete(jobId, employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
