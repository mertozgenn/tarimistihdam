using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class Interest : IEntity
    {
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public int TagId { get; set; }
	}
}

