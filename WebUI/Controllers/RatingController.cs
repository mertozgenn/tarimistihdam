using Business.Abstract;
using Entities.Dtos.Rating;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class RatingController : Controller
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService=ratingService;
        }

        public IActionResult RateEmployee(int employeeId, int rating, string? comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            EmployeeRatingToAddDto employeeRatingToAddDto = new EmployeeRatingToAddDto()
            {
                EmployeeId = employeeId,
                Rating = rating,
                UserId = int.Parse(userId),
                Comment = comment
            };
            var result = _ratingService.AddEmployeeRating(employeeRatingToAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        public IActionResult RateEmployer(int employerId, int rating, string? comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            EmployerRatingToAddDto employerRatingToAddDto = new EmployerRatingToAddDto()
            {
                EmployerId = employerId,
                Rating = rating,
                UserId = int.Parse(userId),
                Comment = comment
            };
            var result = _ratingService.AddEmployerRating(employerRatingToAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
