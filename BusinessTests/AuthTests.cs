using Entities.Dtos.User;

namespace BusinessTests
{
    [TestClass]
    public class AuthTests : TestBase
    {
        EmployeeForRegisterDto employeeForRegisterDto = new EmployeeForRegisterDto
        {
            Email = "test@test.com",
            Name = "Test",
            Password = "123",
            Phone = "1234567890",
            Surname = "Test",
            Tckn = "Test",
        };

        EmployerForRegisterDto employerForRegisterDto = new EmployerForRegisterDto
        {
            CompanyName = "Test",
            Email = "test@test.com",
            Name = "Test",
            Tckn = "Test",
            Password = "123",
            Surname = "Test",
            Phone = "1234567890",
            TaxNumber = "1234567890",
            TaxPlace = "Test",
        };


        [TestMethod]
        public void EmployeeRegister_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.EmployeeRegister(employeeForRegisterDto);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployeeId"));
            Clean();
        }

        [TestMethod]
        public void EmployeeRegister_WithExistingEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployeeRegister(employeeForRegisterDto);
            var result = authService.EmployeeRegister(employeeForRegisterDto);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserAlreadyExists, result.Message);
            Clean();
        }

        [TestMethod]
        public void EmployerRegister_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.EmployerRegister(employerForRegisterDto);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployerId"));
            Clean();
        }

        [TestMethod]
        public void EmployerRegister_WithExistingEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployerRegister(employerForRegisterDto);
            var result = authService.EmployerRegister(employerForRegisterDto);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserAlreadyExists, result.Message);
            Clean();
        }

        [TestMethod]
        public void EmployeeLogin_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployeeRegister(employeeForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = employeeForRegisterDto.Email,
                Password = employeeForRegisterDto.Password
            });
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployeeId"));
            Clean();
        }

        [TestMethod]
        public void EmployeeLogin_WithInvalidPassword_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployeeRegister(employeeForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = employeeForRegisterDto.Email,
                Password = "1234"
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.PasswordError, result.Message);
            Clean();
        }

        [TestMethod]
        public void EmployeeLogin_WithInvalidEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployeeRegister(employeeForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = "",
                Password = employeeForRegisterDto.Password
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserNotFound, result.Message);
            Clean();
        }

        [TestMethod]
        public void EmployerLogin_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployerRegister(employerForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = employerForRegisterDto.Email,
                Password = employerForRegisterDto.Password
            });
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployerId"));
            Clean();
        }

        [TestMethod]
        public void EmployerLogin_WithInvalidPassword_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployerRegister(employerForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = employerForRegisterDto.Email,
                Password = "1234"
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.PasswordError, result.Message);
            Clean();
        }

        [TestMethod]
        public void EmployerLogin_WithInvalidEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployerRegister(employerForRegisterDto);
            var result = authService.Login(new UserForLoginDto
            {
                Email = "",
                Password = employerForRegisterDto.Password
            });
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserNotFound, result.Message);
            Clean();
        }

        [TestCleanup]
        [TestInitialize]
        public void Clean()
        {
            var userDal = _container.Resolve<IUserDal>();
            var employeeDal = _container.Resolve<IEmployeeDal>();
            var employerDal = _container.Resolve<IEmployerDal>();
            var userOperationClaimDal = _container.Resolve<IUserOperationClaimDal>();
            var users = userDal.GetAll(x => x.Email == employeeForRegisterDto.Email);
            foreach (var user in users)
            {
                userOperationClaimDal.DeleteAll(userOperationClaimDal.GetAll(x => x.UserId == user.Id));
                employeeDal.DeleteAll(employeeDal.GetAll(x => x.UserId == user.Id));
                employerDal.DeleteAll(employerDal.GetAll(x => x.UserId == user.Id));
            }
            userDal.DeleteAll(users);
        }
    }
}
