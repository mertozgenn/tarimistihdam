using Entities.Dtos.Job;

namespace WebUI.Models
{
    public class MyFavoritesModel
    {
        public string Message { get; set; }
        public List<JobDto> Favorites { get; set; }
    }
}
