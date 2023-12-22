﻿using Entities.Concrete;
using Entities.Dtos.Job;
using Entities.Dtos.Rating;
using Entities.Dtos.User;

namespace WebUI.Models
{
    public class EmployerProfileModel
    {
        public EmployerInformationDto EmployerInformation { get; set; }
        public List<EmployerRatingDto> Ratings { get; set; }
        public List<JobDto> Jobs { get; set; }
        public string? Message { get; set; }
    }
}
