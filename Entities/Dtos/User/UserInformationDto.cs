using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class UserInformationDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Tckn { get; set; }
        public string ProfilePhoto { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
