using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using Core.Concrete.Entities;
using DataAccess.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new Context())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public string GetType(int userId)
        {
            using (Context context = new Context())
            {
                bool isEmployer = context.Employers.Any(x => x.UserId == userId);
                bool isEmployee = context.Employees.Any(x => x.UserId == userId);
                if (isEmployer)
                {
                    return "Employer";
                }
                else if (isEmployee)
                {
                    return "Employee";
                }
                else
                {
                    return "User";
                }
            }
        }
    }
}

