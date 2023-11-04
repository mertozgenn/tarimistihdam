using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class Favorite : IEntity
    {
		public int Id { get; set; }
		public int JobId { get; set; }
		public int EmployeeId { get; set; }
		public DateTime AddedDate { get; set; }
	}
}

