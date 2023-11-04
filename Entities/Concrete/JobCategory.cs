using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class JobCategory : IEntity
    {
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

