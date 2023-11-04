using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class WorkExperience : IEntity
    {
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}

