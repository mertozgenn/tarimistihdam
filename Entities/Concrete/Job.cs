using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstract.Entities;

namespace Entities.Concrete
{
	public class Job : IEntity
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        [Column(TypeName = "decimal(18,8)")]
        public decimal DailyWage { get; set; }
		public int EmployeeCount { get; set; }
		public DateTime PublishDate { get; set; }
		public bool IsActive { get; set; }
		public int CityId { get; set; }
		public int DistrictId { get; set; }
		public int CategoryId { get; set; }
		public string? Image { get; set; }
		public string? NlpTags { get; set; }
		public int EmployerId { get; set; }
		public string Status { get; set; }
	}
}

