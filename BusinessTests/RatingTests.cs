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
            Tckn = "1"
        };

        EmployeeForRegisterDto employeeToRegister = new()
        {
            Email = "test2@test.com",
            Name = "Test",
            Password = "1",
            Phone = "1",
            Surname = "Test",
            Tckn = "1"
        };

        [TestMethod]
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
                Status = "finished"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
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
            CleanUp();
        }

        [TestMethod]
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
                Status = "finished"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
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
            CleanUp();
        }

        [TestMethod]
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
                Status = "finished"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
            });
            ratingManager.AddEmployeeRating(new Entities.Dtos.Rating.EmployeeRatingToAddDto
            {
                Comment = "Test",
                EmployeeId = employee.Id,
                Rating = 5,
                UserId = employerUser.Id
            });
            var ratingDal = _container.Resolve<IEmployeeRatingDal>();
            var rating = ratingDal.GetAll(x => x.UserId == employerUser.Id).FirstOrDefault();
            // Act
            var result = ratingManager.DeleteEmployeeRating(rating.Id, employerUser.Id);
            // Assert
            Assert.IsTrue(result.Success);
            CleanUp();
        }

        [TestMethod]
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
                Status = "finished"
            });
            var job = jobDal.GetAll(x => x.EmployerId == employer.Id).FirstOrDefault();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.Add(new JobApplication
            {
                EmployeeId = employee.Id,
                JobId = job.Id,
                ApplicationDate = DateTime.Now,
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
            CleanUp();
        }

        [TestCleanup]
        public void CleanUp()
        {
            var userDal = _container.Resolve<IUserDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var userOperationClaimDal = _container.Resolve<IUserOperationClaimDal>();
            var employeeRatingDal = _container.Resolve<IEmployeeRatingDal>();
            var employerRatingDal = _container.Resolve<IEmployerRatingDal>();
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            var jobDal = _container.Resolve<IJobDal>();
            var users = userDal.GetAll(x => x.Email == employeeToRegister.Email);
            foreach (var user in users)
            {
                var employers = employerDal.GetAll(x => x.UserId == user.Id);
                foreach (var employer in employers)
                {
                    jobDal.DeleteAll(jobDal.GetAll(x => x.EmployerId == employer.Id));
                }
                var employees = employeeDal.GetAll(x => x.UserId == user.Id);
                foreach (var employee in employees)
                {
                    jobApplicationDal.DeleteAll(jobApplicationDal.GetAll(x => x.EmployeeId == employee.Id));
                }
                userOperationClaimDal.DeleteAll(userOperationClaimDal.GetAll(x => x.UserId == user.Id));
                employeeDal.DeleteAll(employeeDal.GetAll(x => x.UserId == user.Id));
                employerDal.DeleteAll(employerDal.GetAll(x => x.UserId == user.Id));
                employerRatingDal.DeleteAll(employerRatingDal.GetAll(x => x.UserId == user.Id));
                var data = employeeRatingDal.GetAll(x => x.UserId == user.Id);
                employeeRatingDal.DeleteAll(data);
            }
            userDal.DeleteAll(users);
            users = userDal.GetAll(x => x.Email == employerToRegister.Email);
            foreach (var user in users)
            {
                var employers = employerDal.GetAll(x => x.UserId == user.Id);
                foreach (var employer in employers)
                {
                    jobDal.DeleteAll(jobDal.GetAll(x => x.EmployerId == employer.Id));
                }
                var employees = employeeDal.GetAll(x => x.UserId == user.Id);
                foreach (var employee in employees)
                {
                    jobApplicationDal.DeleteAll(jobApplicationDal.GetAll(x => x.EmployeeId == employee.Id));
                }
                userOperationClaimDal.DeleteAll(userOperationClaimDal.GetAll(x => x.UserId == user.Id));
                employeeDal.DeleteAll(employeeDal.GetAll(x => x.UserId == user.Id));
                employerDal.DeleteAll(employerDal.GetAll(x => x.UserId == user.Id));
                employeeRatingDal.DeleteAll(employeeRatingDal.GetAll(x => x.UserId == user.Id));
                employerRatingDal.DeleteAll(employerRatingDal.GetAll(x => x.UserId == user.Id));
            }
            userDal.DeleteAll(users);
        }
    }
}
