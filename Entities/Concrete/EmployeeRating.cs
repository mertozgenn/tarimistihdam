﻿using System;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class EmployeeRating : IEntity
    {
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public short Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
	}
}
