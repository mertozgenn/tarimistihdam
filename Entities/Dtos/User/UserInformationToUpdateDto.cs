using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class UserInformationToUpdateDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Tckn { get; set; }
        public IFormFile? NewProfilePhoto { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPasswordAgain { get; set; }
    }
}
