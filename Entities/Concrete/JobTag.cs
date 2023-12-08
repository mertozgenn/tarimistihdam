using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class JobTag : IEntity
    {
		public int Id { get; set; }
		public string DisplayName { get; set; }
        public string Key { get; set; }
    }
}

