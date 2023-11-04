using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeRatingDal : EfEntityRepositoryBase<EmployeeRating, Context>, IEmployeeRatingDal
    {
        public bool CanRate(int employeeId, int employerId)
        {
            using(Context context = new Context())
            {
                var employerFinishedJobIds = context.Jobs.Where(x => x.EmployerId == employerId &&
                                                                x.Status == "finished")
                                                      .Select(x => x.Id);
                var raterFinishedJobApplicants = context.JobApplications.Where(x =>
                                                   employerFinishedJobIds.Contains(x.JobId) &&
                                                   x.EmployeeId == employeeId
                                                 );

                return raterFinishedJobApplicants.Any();
            }
        }
    }
}

