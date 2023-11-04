using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class City: IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Image { get; set; }
	}
}

