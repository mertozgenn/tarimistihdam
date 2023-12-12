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
                var data = from employee in context.Employees
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
                               EmployeeId = employee.Id
                           };
                return data.SingleOrDefault();
            }
        }
    }
}

