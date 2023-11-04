using System;
using System.Linq.Expressions;
using System.Text.Json;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfJobDal : EfEntityRepositoryBase<Job, Context>, IJobDal
    {
        public List<JobDto> GetAllDto(Expression<Func<JobDto, bool>>? filter = null)
        {
            using(Context context = new Context())
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
                                 City = (from city in context.Cities
                                         where city.Id == job.CityId
                                         select city.Name).First(),
                                 District = (from district in context.Districts
                                             where district.Id == job.DistrictId
                                             select district.Name).First(),
                                 Employer = user.Name + " " + user.Surname,
                                 DailyWage = job.DailyWage,
                                 Description = job.Description,
                                 PublishDate = job.PublishDate,
                                 Title = job.Title,
                                 Tags = job.NlpTags.Split(",", StringSplitOptions.None).ToList(),
                                 Id = job.Id,
                                 EmployerProfilePhoto = user.ProfilePhoto
                             });

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return query.ToList();
            }
        }
    }
}

