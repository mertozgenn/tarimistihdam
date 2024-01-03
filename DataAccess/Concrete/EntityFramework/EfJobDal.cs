using System;
using System.Text.Json;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfJobDal : EfEntityRepositoryBase<Job, Context>, IJobDal
    {
        public List<JobDto> GetAllDto(Predicate<JobDto>? filter = null, JobFilterDto? jobFilterDto = null)
        {
            using (Context context = new Context())
            {
                var query = (from job in context.Jobs
                             join employer in context.Employers
                             on job.EmployerId equals employer.Id
                             join user in context.Users
                             on employer.UserId equals user.Id
                             select new JobDto
                             {
                                 Category = (from category in context.JobCategories
                                             where category.Id == job.CategoryId
                                             select category.Name).First(),
                                 CategoryId = job.CategoryId,
                                 City = (from city in context.Cities
                                         where city.Id == job.CityId
                                         select city.Name).First(),
                                 CityId = job.CityId,
                                 District = (from district in context.Districts
                                             where district.Id == job.DistrictId
                                             select district.Name).First(),
                                 Employer = user.Name + " " + user.Surname,
                                 EmployerId = employer.Id,
                                 DailyWage = job.DailyWage,
                                 Description = job.Description,
                                 PublishDate = job.PublishDate,
                                 Title = job.Title,
                                 NlpTags = job.NlpTags ?? "",
                                 Id = job.Id,
                                 EmployerProfilePhoto = user.ProfilePhoto,
                                 Image = job.Image ?? "/Assets/imgs/home2.jpg",
                                 EmployeeCount = job.EmployeeCount,
                                 DistrictId = job.DistrictId,
                                 Status = job.Status,
                                 IsActive = job.IsActive
                             }).ToList();

                var data = query;
                var jobTags = context.JobTags.ToList();
                foreach (var job in data)
                {
                    if (string.IsNullOrEmpty(job.NlpTags))
                    {
                        job.Tags = new List<JobTag>();
                        continue;
                    }
                    var nlpTags = job.NlpTags.Split(",", StringSplitOptions.None).ToList();
                    if (nlpTags != null && nlpTags.Any())
                    {
                        job.Tags = nlpTags.Select(x => jobTags.Where(y => y.Key == x).Select(y => y).FirstOrDefault()).ToList();
                    }
                    else
                    {
                        job.Tags = new List<JobTag>();
                    }
                }
                if (filter != null)
                {
                    data = data.FindAll(filter);
                }
                if (jobFilterDto != null)
                {
                    data = data.FindAll(x => (jobFilterDto.CategoryId != null ? jobFilterDto.CategoryId == x.CategoryId : true) &&
                                             (jobFilterDto.CityId != null ? jobFilterDto.CityId == x.CityId : true) &&
                                             (jobFilterDto.DistrictId != null ? jobFilterDto.DistrictId == x.DistrictId : true) &&
                                             (jobFilterDto.TagKeys != null ? x.Tags.Any(x => jobFilterDto.TagKeys.Contains(x.Key)) : true) &&
                                             (jobFilterDto.MinWage != null ? x.DailyWage >= jobFilterDto.MinWage : true) &&
                                             ((jobFilterDto.MaxWage != null && jobFilterDto.MaxWage != -1) ? x.DailyWage <= jobFilterDto.MaxWage : true)
                        );
                }

                return data.OrderByDescending(x => x.PublishDate).ToList();
            }
        }
    }
}

