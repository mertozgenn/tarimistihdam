using DataAccess.Concrete;
using Entities.Dtos.User;

namespace BusinessTests
{
    [TestClass]
    public class AuthTests : TestBase
    {
        EmployeeForRegisterDto employeeForRegisterDto = new EmployeeForRegisterDto
        {
            Email = "tst@test.com",
            Name = "Test",
            Password = "123",
            Phone = "1234567890",
            Surname = "Test",
            Tckn = "Test",
            RePassword = "123",
        };

        EmployerForRegisterDto employerForRegisterDto = new EmployerForRegisterDto
        {
            CompanyName = "Test",
            Email = "tst@test.com",
            Name = "Test",
            Tckn = "Test",
            Password = "123",
            Surname = "Test",
            Phone = "1234567890",
            TaxNumber = "1234567890",
            TaxPlace = "Test",
            RePassword = "123"
        };


        [CustomTestMethod]
        public void EmployeeRegister_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.EmployeeRegister(employeeForRegisterDto);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployeeId"));
        }

        [CustomTestMethod]
        public void EmployeeRegister_WithExistingEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployeeRegister(employeeForRegisterDto);
            var result = authService.EmployeeRegister(employeeForRegisterDto);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserAlreadyExists, result.Message);
        }

        [CustomTestMethod]
        public void EmployerRegister_WithValidData_ShouldReturnSuccess()
        {
            var authService = _container.Resolve<IAuthService>();
            var result = authService.EmployerRegister(employerForRegisterDto);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Any(x => x.Type == "EmployerId"));
        }

        [CustomTestMethod]
        public void EmployerRegister_WithExistingEmail_ShouldReturnError()
        {
            var authService = _container.Resolve<IAuthService>();
            authService.EmployerRegister(employerForRegisterDto);
            var result = authService.EmployerRegister(employerForRegisterDto);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.UserAlreadyExists, result.Message);
        }

        [CustomTestMethod]
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
        }

        [CustomTestMethod]
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
        }

        [CustomTestMethod]
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
        }

        [CustomTestMethod]
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
        }

        [CustomTestMethod]
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
        }

        [CustomTestMethod]
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
        }
    }
}
