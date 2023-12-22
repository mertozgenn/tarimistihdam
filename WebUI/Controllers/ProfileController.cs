using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.User;
using Entities.Dtos.WorkExperience;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IEmployerService _employerService;
        private IEmployeeService _employeeService;
        private IUserService _userService;
        private IInterestService _interestService;
        private IWorkExperienceService _workExperienceService;
        private IJobTagService _jobTagService;
        private IJobService _jobService;
        private IRatingService _ratingService;

        public ProfileController(IEmployerService employerService, IEmployeeService employeeService, 
            IUserService userService, IInterestService interestService, IWorkExperienceService workExperienceService, 
            IJobTagService jobTagService, IJobService jobService, IRatingService ratingService)
        {
            _employerService=employerService;
            _employeeService=employeeService;
            _userService=userService;
            _interestService=interestService;
            _workExperienceService=workExperienceService;
            _jobTagService=jobTagService;
            _jobService=jobService;
            _ratingService=ratingService;
        }

        [HttpGet("profilim")]
        public IActionResult Index()
        {
            var userRole = HttpContext.User.IsInRole("Employer") ? "Employer" : "Employee";
            if (userRole == "Employer")
            {
                var employerIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
                var employerId = int.Parse(employerIdClaim.Value);
                var employerInformationResult = _employerService.GetEmployerInformation(employerId);
                var employerInformation = employerInformationResult.Data;
                EmployerInformationToUpdateDto employerInformationToUpdateDto = new EmployerInformationToUpdateDto()
                {
                    CompanyName = employerInformation.CompanyName,
                    Email = employerInformation.Email,
                    EmployerId = employerInformation.EmployerId,
                    Name = employerInformation.Name,
                    Phone = employerInformation.Phone,
                    Surname = employerInformation.Surname,
                    TaxNumber = employerInformation.TaxNumber,
                    TaxPlace = employerInformation.TaxPlace,
                    Tckn = employerInformation.Tckn,
                    UserId = employerInformation.UserId,
                    ProfilePhoto = employerInformation.ProfilePhoto
                };
                EmployerMyProfileModel employerProfileModel = new EmployerMyProfileModel()
                {
                    EmployerInformation = employerInformationToUpdateDto,
                    Message = employerInformationResult.Message
                };
                return View("EmployerMyProfile", employerProfileModel);
            }
            else
            {
                var employeeIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
                var employeeId = int.Parse(employeeIdClaim.Value);
                var employeeInformationResult = _employeeService.GetEmployeeInformation(employeeId);
                var employeeInformation = employeeInformationResult.Data;
                var interestsResult = _interestService.GetByEmployeeId(employeeId);
                var workExperiencesResult = _workExperienceService.GetByEmployeeId(employeeId);
                var jobTags = _jobTagService.GetJobTags();
                EmployeeInformationToUpdateDto employeeInformationToUpdateDto = new EmployeeInformationToUpdateDto()
                {
                    Email = employeeInformation.Email,
                    EmployeeId = employeeInformation.EmployeeId,
                    Name = employeeInformation.Name,
                    Phone = employeeInformation.Phone,
                    Surname = employeeInformation.Surname,
                    Tckn = employeeInformation.Tckn,
                    UserId = employeeInformation.UserId,
                    ProfilePhoto = employeeInformation.ProfilePhoto,
                };
                EmployeeMyProfileModel employeeProfileModel = new EmployeeMyProfileModel()
                {
                    EmployeeInformation = employeeInformationToUpdateDto,
                    Message = employeeInformationResult.Message,
                    Interests = interestsResult.Data,
                    WorkExperiences = workExperiencesResult.Data,
                    Tags = jobTags
                };
                return View("EmployeeMyProfile", employeeProfileModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateProfilePhoto(IFormFile profilePhoto)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            _userService.UpdateProfilePhoto(userId, profilePhoto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateEmployerInformation(EmployerInformationToUpdateDto employerInformation)
        {
            employerInformation.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            employerInformation.EmployerId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployerId").Value);
            var result = _employerService.UpdateEmployerInformation(employerInformation);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                EmployerMyProfileModel employerProfileModel = new EmployerMyProfileModel()
                {
                    EmployerInformation = employerInformation,
                    Message = result.Message
                };
                return View("EmployerMyProfile", employerProfileModel);
            }
        }

        [HttpPost]
        public IActionResult UpdateEmployeeInformation(EmployeeInformationToUpdateDto employeeInformation)
        {
            employeeInformation.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            employeeInformation.EmployeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId").Value);
            var result = _employeeService.UpdateEmployeeInformation(employeeInformation);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                EmployeeMyProfileModel employeeProfileModel = new EmployeeMyProfileModel()
                {
                    EmployeeInformation = employeeInformation,
                    Message = result.Message
                };
                return View("EmployeeMyProfile", employeeProfileModel);
            }
        }

        [HttpGet]
        public IActionResult AddInterest([FromQuery] int id)
        {
            Interest interest = new Interest()
            {
                EmployeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId").Value),
                TagId = id
            };
            var result = _interestService.Add(interest);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult DeleteInterest([FromQuery] int id)
        {
            var result = _interestService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public IActionResult AddWorkExperience(WorkExperienceToAddDto workExperience)
        {
            workExperience.EmployeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId").Value);
            var result = _workExperienceService.Add(workExperience);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteWorkExperience([FromQuery] int id)
        {
            var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "EmployeeId").Value);
            var result = _workExperienceService.Delete(id, employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("profil/{id}")]
        public IActionResult Profile(int id)
        {
            var userRole = _userService.GetUserType(id);
            if (userRole == "Employer")
            {
                int employerId = _employerService.GetByUserId(id).Id;
                var employerInformationResult = _employerService.GetEmployerInformation(employerId);
                var employerInformation = employerInformationResult.Data;
                var jobsResult = _jobService.GetByEmployerId(false, employerId);
                var ratings = _ratingService.GetEmployerRatings(employerId);
                EmployerProfileModel employerProfileModel = new EmployerProfileModel()
                {
                    EmployerInformation = employerInformation,
                    Message = employerInformationResult.Message,
                    Jobs = jobsResult.Data,
                    Ratings = ratings.Data
                };
                return View("EmployerProfile", employerProfileModel);
            }
            else
            {
                int employeeId = _employeeService.GetByUserId(id).Id;
                var employeeInformationResult = _employeeService.GetEmployeeInformation(employeeId);
                var employeeInformation = employeeInformationResult.Data;
                var interestsResult = _interestService.GetByEmployeeId(employeeId);
                var workExperiencesResult = _workExperienceService.GetByEmployeeId(employeeId);
                var ratings = _ratingService.GetEmployeeRatings(employeeId);
                EmployeeProfileModel employeeProfileModel = new EmployeeProfileModel()
                {
                    EmployeeInformation = employeeInformation,
                    Message = employeeInformationResult.Message,
                    Interests = interestsResult.Data,
                    WorkExperiences = workExperiencesResult.Data,
                    Ratings = ratings.Data
                };
                return View("EmployeeProfile", employeeProfileModel);
            }
        }
    }
}
