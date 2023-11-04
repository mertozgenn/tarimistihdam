using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class District : IEntity
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public int CityId { get; set; }
	}
}

