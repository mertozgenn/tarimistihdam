using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class Employer : IEntity
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public string? TaxPlace { get; set; }
		public string? TaxNumber { get; set; }
		public string? CompanyName { get; set; }
	}
}

