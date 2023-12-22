using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.User;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee, Context>, IEmployeeDal
    {
        public EmployeeInformationDto GetEmployeeInformation(int employeeId)
        {
            using (Context context = new Context())
            {
                var data = (from employee in context.Employees
                           join user in context.Users
                           on employee.UserId equals user.Id
                           where employee.Id == employeeId
                           select new EmployeeInformationDto
                           {
                               Email = user.Email,
                               Name = user.Name,
                               Phone = user.Phone,
                               Surname = user.Surname,
                               ProfilePhoto = user.ProfilePhoto ?? "/Assets/imgs/avatar/profile.png",
                               Tckn = user.Tckn,
                               UserId = user.Id,
                               EmployeeId = employee.Id,
                               Interests = (from tag in context.JobTags
                                            join interest in context.Interests
                                            on tag.Id equals interest.TagId
                                            where interest.EmployeeId == employeeId
                                            select tag).ToList(),
                           }).Single();
                data.RatingCount = context.EmployeeRatings.Count(x => x.EmployeeId == employeeId);
                data.Rating = data.RatingCount != 0 ? context.EmployeeRatings.Where(x => x.EmployeeId == employeeId).Average(x => x.Rating) : 0;
                return data;
            }
        }
    }
}

