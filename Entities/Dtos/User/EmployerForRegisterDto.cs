using System;
namespace Entities.Dtos.User
{
	public class EmployerForRegisterDto: UserForRegisterDto
	{
		public string? TaxPlace { get; set; }
		public string? TaxNumber { get; set; }
		public string? CompanyName { get; set; }
	}
}

