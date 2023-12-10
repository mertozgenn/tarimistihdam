using System;
namespace Entities.Dtos.User
{
	public class UserForRegisterDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
        public string RePassword { get; set; }
        public string Email { get; set; }
		public string Phone { get; set; }
		public string Tckn { get; set; }
	}
}

