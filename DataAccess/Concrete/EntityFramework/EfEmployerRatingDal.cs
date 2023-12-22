using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using Core.Concrete.Entities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Rating;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployerRatingDal : EfEntityRepositoryBase<EmployerRating, Context>, IEmployerRatingDal
    {
        public bool CanRate(int employerId, int userId)
        {
            using(Context context = new Context())
            {
                var employee = context.Employees.SingleOrDefault(x => x.UserId == userId);
                var employerFinishedJobIds = context.Jobs.Where(x => x.EmployerId == employerId &&
                                                                x.Status == "Kapatıldı")
                                                      .Select(x => x.Id).ToList();
                var raterFinishedJobApplicants = context.JobApplications.Where(x =>
                                                   employerFinishedJobIds.Contains(x.JobId) &&
                                                   x.EmployeeId == employee.Id &&
                                                   x.IsApproved == true
                                                 ).ToList();

                bool isRated = context.EmployerRatings.Any(x => x.EmployerId == employerId && x.UserId == userId);

                return raterFinishedJobApplicants.Any() && !isRated;
            }
        }

        public List<EmployerRatingDto> GetAllDto(int employerId)
        {
            using (Context context = new Context())
            {
                var query = (from rating in context.EmployerRatings
                             join user in context.Users
                             on rating.UserId equals user.Id
                             where rating.EmployerId == employerId
                             select new EmployerRatingDto
                             {
                                 Comment = rating.Comment,
                                 Date = rating.Date,
                                 Id = rating.Id,
                                 Name = user.Name,
                                 Rating = rating.Rating,
                                 EmployerId = rating.EmployerId,
                                 Surname = user.Surname,
                                 UserId = rating.UserId
                             });
                return query.ToList();
            }
        }
    }
}

