using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class Employee : IEntity
    {
		public int Id { get; set; }
		public int UserId { get; set; }
	}
}

