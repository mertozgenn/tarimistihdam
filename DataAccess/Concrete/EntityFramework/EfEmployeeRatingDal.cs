using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;

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
                                                                x.Status == "finished")
                                                      .Select(x => x.Id).ToList();
                var raterFinishedJobApplicants = context.JobApplications.Where(x =>
                                                   employerFinishedJobIds.Contains(x.JobId) &&
                                                   x.EmployeeId == employeeId
                                                 ).ToList();

                return raterFinishedJobApplicants.Any();
            }
        }
    }
}

