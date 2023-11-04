using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class JobApplication : IEntity
    {
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public int JobId { get; set; }
		public DateTime ApplicationDate { get; set; }
	}
}

