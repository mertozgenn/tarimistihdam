using Entities.Dtos.User;

namespace WebUI.Models
{
    public class CandidatesModel
    {
        public List<CandidateDto> Candidates { get; set; }
        public int JobId { get; set; }
        public string Message { get; set; }
    }
}
