using Business.Abstract;
using Entities.Dtos.Job;
using Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class JobApplicationController : Controller
    {
        private IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService=jobApplicationService;
        }

        public IActionResult ApplyJob(int jobId)
        {
            var employeeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            if (employeeClaim == null)
            {
                return Redirect("/giris-yap");
            }
            var employeeId = int.Parse(employeeClaim.Value);
            var result = _jobApplicationService.Apply(jobId, employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("basvurular")]
        public IActionResult EmployeeAppliedJobs()
        {
            var employeeClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
            int employeeId = int.Parse(employeeClaim.Value);
            var result = _jobApplicationService.GetAppliedJobs(employeeId);
            AppliedJobsModel model = new AppliedJobsModel()
            {
                Jobs = result.Data != null ? result.Data : new List<JobDto>(),
                Message = result.Message
            };
            return View("AppliedJobs", model);
        }

        [Route("is-ilanlarim/basvurular/{jobId}")]
        public IActionResult Candidates(int jobId)
        {
            var employerClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
            int employerId = int.Parse(employerClaim.Value);
            var result = _jobApplicationService.GetCandidates(jobId, employerId);
            CandidatesModel model = new CandidatesModel()
            {
                Candidates = result.Data != null ? result.Data : new List<CandidateDto>(),
                Message = result.Message,
                JobId = jobId
            };
            return View(model);
        }

        public IActionResult ApproveApplication([FromQuery]int jobId, [FromQuery]int employeeId)
        {
            var employerClaim = User.Claims.FirstOrDefault(x => x.Type == "EmployerId");
            int employerId = int.Parse(employerClaim.Value);
            var result = _jobApplicationService.ApproveApplication(jobId, employeeId, employerId);
            return Redirect($"/is-ilanlarim/basvurular/{jobId}");
        }
    }
}
