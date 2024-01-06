using Entities.Concrete;
using Entities.Dtos.User;

namespace BusinessTests
{
    [TestClass]
    public class RatingTests: TestBase
    {
        EmployerForRegisterDto employerToRegister = new()
        {
            CompanyName = "Test Company",
            Email = "test@test.com",
            Name = "Test",
            Password = "1",
            Phone = "q",
            Surname = "Test",
            TaxNumber = "1",
            TaxPlace = "1",
            Tckn = "1",
            RePassword = "1"
        };

        EmployeeForRegisterDto employeeToRegister = new()
        {
            Email = "test2@test.com",
            Name = "Test",
            Password = "1",
            Phone = "1",
            Surname = "Test",
            Tckn = "1",
            RePassword = "1",
        };

        [CustomTestMethod]
        public void AddEmployeeRating_ShouldAddEmployeeRating()
        {
            // Arrange
            var ratingManager = _container.Resolve<IRatingService>();
            var authManager = _container.Resolve<IAuthService>();
            authManager.EmployeeRegister(employeeToRegister);
            authManager.EmployerRegister(employerToRegister);

            var userDal = _container.Resolve<IUserDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerUser = userDal.Get(x => x.Email == employerToRegister.Email);
            var employeeUser = userDal.Get(x => x.Email == employeeToRegister.Email);
            var employer = employerDal.Get(x => x.UserId == employerUser.Id);
            var employee = employeeDal.Get(x => x.UserId == employeeUser.Id);
            var jobDal = _container.Resolve<IJobDal>();
            jobDal.Add(new Job
            {
                EmployerId = employer.Id,
                CategoryId = 0,
                CityId = 0,
                DailyWage = 0,
                Description = "Test",
                DistrictId = 0,
                EmployeeCount = 0,
                Image = "",
                IsActive = true,
                NlpTags = "",
                PublishDate = DateTime.Now,
                Title = "Test",
                Status = "Kapatıldı"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
                IsApproved = true
            });
            // Act
            var result = ratingManager.AddEmployeeRating(new Entities.Dtos.Rating.EmployeeRatingToAddDto
            {
                Comment = "Test",
                EmployeeId = employee.Id,
                Rating = 5,
                UserId = employerUser.Id
            });
            // Assert
            Assert.IsTrue(result.Success);
        }

        [CustomTestMethod]
        public void AddEmployerRating_ShouldAddEmployerRating()
        {
            // Arrange
            var ratingManager = _container.Resolve<IRatingService>();
            var authManager = _container.Resolve<IAuthService>();
            authManager.EmployeeRegister(employeeToRegister);
            authManager.EmployerRegister(employerToRegister);

            var userDal = _container.Resolve<IUserDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerUser = userDal.Get(x => x.Email == employerToRegister.Email);
            var employeeUser = userDal.Get(x => x.Email == employeeToRegister.Email);
            var employer = employerDal.Get(x => x.UserId == employerUser.Id);
            var employee = employeeDal.Get(x => x.UserId == employeeUser.Id);
            var jobDal = _container.Resolve<IJobDal>();
            jobDal.Add(new Job
            {
                EmployerId = employer.Id,
                CategoryId = 0,
                CityId = 0,
                DailyWage = 0,
                Description = "Test",
                DistrictId = 0,
                EmployeeCount = 0,
                Image = "",
                IsActive = true,
                NlpTags = "",
                PublishDate = DateTime.Now,
                Title = "Test",
                Status = "Kapatıldı"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
                IsApproved = true
            });
            // Act
            var result = ratingManager.AddEmployerRating(new Entities.Dtos.Rating.EmployerRatingToAddDto
            {
                Comment = "Test",
                EmployerId = employer.Id,
                Rating = 5,
                UserId = employeeUser.Id
            });
            // Assert
            Assert.IsTrue(result.Success);
        }

        [CustomTestMethod]
        public void DeleteEmployeeRating_ShouldDeleteEmployeeRating()
        {
            // Arrange
            var ratingManager = _container.Resolve<IRatingService>();
            var authManager = _container.Resolve<IAuthService>();
            authManager.EmployeeRegister(employeeToRegister);
            authManager.EmployerRegister(employerToRegister);

            var userDal = _container.Resolve<IUserDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerUser = userDal.Get(x => x.Email == employerToRegister.Email);
            var employeeUser = userDal.Get(x => x.Email == employeeToRegister.Email);
            var employer = employerDal.Get(x => x.UserId == employerUser.Id);
            var employee = employeeDal.Get(x => x.UserId == employeeUser.Id);
            var jobDal = _container.Resolve<IJobDal>();
            jobDal.Add(new Job
            {
                EmployerId = employer.Id,
                CategoryId = 0,
                CityId = 0,
                DailyWage = 0,
                Description = "Test",
                DistrictId = 0,
                EmployeeCount = 0,
                Image = "",
                IsActive = true,
                NlpTags = "",
                PublishDate = DateTime.Now,
                Title = "Test",
                Status = "Kapatıldı"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
                IsApproved = true
            });
            ratingManager.AddEmployeeRating(new Entities.Dtos.Rating.EmployeeRatingToAddDto
            {
                Comment = "Test",
                EmployeeId = employee.Id,
                Rating = 5,
                UserId = employerUser.Id,
            });
            var ratingDal = _container.Resolve<IEmployeeRatingDal>();
            var rating = ratingDal.GetAll(x => x.UserId == employerUser.Id).FirstOrDefault();
            // Act
            var result = ratingManager.DeleteEmployeeRating(rating.Id, employerUser.Id);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [CustomTestMethod]
        public void DeleteEmployerRating_ShouldDeleteEmployerRating()
        {
            // Arrange
            var ratingManager = _container.Resolve<IRatingService>();
            var authManager = _container.Resolve<IAuthService>();
            authManager.EmployeeRegister(employeeToRegister);
            authManager.EmployerRegister(employerToRegister);

            var userDal = _container.Resolve<IUserDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerUser = userDal.Get(x => x.Email == employerToRegister.Email);
            var employeeUser = userDal.Get(x => x.Email == employeeToRegister.Email);
            var employer = employerDal.Get(x => x.UserId == employerUser.Id);
            var employee = employeeDal.Get(x => x.UserId == employeeUser.Id);
            var jobDal = _container.Resolve<IJobDal>();
            jobDal.Add(new Job
            {
                EmployerId = employer.Id,
                CategoryId = 0,
                CityId = 0,
                DailyWage = 0,
                Description = "Test",
                DistrictId = 0,
                EmployeeCount = 0,
                Image = "",
                IsActive = true,
                NlpTags = "",
                PublishDate = DateTime.Now,
                Title = "Test",
                Status = "Kapatıldı"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
                IsApproved = true
            });
            ratingManager.AddEmployerRating(new Entities.Dtos.Rating.EmployerRatingToAddDto
            {
                Comment = "Test",
                EmployerId = employer.Id,
                Rating = 5,
                UserId = employeeUser.Id
            });
            var ratingDal = _container.Resolve<IEmployerRatingDal>();
            var rating = ratingDal.GetAll(x => x.UserId == employeeUser.Id).FirstOrDefault();
            // Act
            var result = ratingManager.DeleteEmployerRating(rating.Id, employeeUser.Id);
            // Assert
            Assert.IsTrue(result.Success);
        }
    }
}
