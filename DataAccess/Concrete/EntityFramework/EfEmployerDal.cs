using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.User;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployerDal : EfEntityRepositoryBase<Employer, Context>, IEmployerDal
    {
        public EmployerInformationDto GetEmployerInformation(int employerId)
        {
            using (Context context = new Context())
            {
                var data = (from employer in context.Employers
                           join user in context.Users
                           on employer.UserId equals user.Id
                           where employer.Id == employerId
                           select new EmployerInformationDto
                           {
                               Email = user.Email,
                               Name = user.Name,
                               Phone = user.Phone,
                               Surname = user.Surname,
                               ProfilePhoto = user.ProfilePhoto ?? "/Assets/imgs/avatar/profile.png",
                               Tckn = user.Tckn,
                               CompanyName = employer.CompanyName,
                               TaxNumber = employer.TaxNumber,
                               TaxPlace = employer.TaxPlace,
                               EmployerId = employer.Id,
                               UserId = user.Id,
                           }).Single();
                data.RatingCount = context.EmployerRatings.Where(x => x.EmployerId == data.EmployerId).Count();
                data.Rating = data.RatingCount > 0 ? context.EmployerRatings.Where(x => x.EmployerId == data.EmployerId).Average(x => x.Rating) : 0;
                return data;
            }
        }
    }
}

