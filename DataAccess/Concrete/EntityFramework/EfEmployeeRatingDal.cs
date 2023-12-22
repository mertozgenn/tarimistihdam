using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Rating;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeRatingDal : EfEntityRepositoryBase<EmployeeRating, Context>, IEmployeeRatingDal
    {
        public bool CanRate(int employeeId, int userId)
        {
            using(Context context = new Context())
            {
                var employerUser = context.Users.SingleOrDefault(x => x.Id == userId);
                var employerId = context.Employers.SingleOrDefault(x => x.UserId == employerUser.Id).Id;
                var employerFinishedJobIds = context.Jobs.Where(x => x.EmployerId == employerId &&
                                                                x.Status == "Kapatıldı")
                                                      .Select(x => x.Id).ToList();
                var raterFinishedJobApplicants = context.JobApplications.Where(x =>
                                                   employerFinishedJobIds.Contains(x.JobId) &&
                                                   x.EmployeeId == employeeId &&
                                                   x.IsApproved == true
                                                 ).ToList();
                bool isRated = context.EmployeeRatings.Any(x => x.EmployeeId == employeeId &&
                                                                               x.UserId == userId);
                return raterFinishedJobApplicants.Any() && !isRated;
            }
        }

        public List<EmployeeRatingDto> GetAllDto(int employeeId)
        {
            using (Context context = new Context())
            {
                var query = (from rating in context.EmployeeRatings
                             join user in context.Users
                             on rating.UserId equals user.Id
                             where rating.EmployeeId == employeeId
                             select new EmployeeRatingDto
                             {
                                 Comment = rating.Comment,
                                 Date = rating.Date,
                                 EmployeeId = rating.EmployeeId,
                                 Id = rating.Id,
                                 Name = user.Name,
                                 Rating = rating.Rating,
                                 Surname = user.Surname,
                                 UserId = rating.UserId
                             });
                return query.ToList();
            }
        }
    }
}

