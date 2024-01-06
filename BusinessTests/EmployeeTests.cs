using Entities.Dtos.User;
using System.Security.Claims;

namespace BusinessTests
{
    [TestClass]
    public class EmployeeTests: TestBase
    {
        EmployeeForRegisterDto employeeForRegisterDto = new EmployeeForRegisterDto
        {
            Email = "test@₺est.com",
            Name = "Test",
            Password = "123",
            Phone = "1234567890",
            Surname = "Test",
            Tckn = "12345678901",
            RePassword = "123",
        };

        [CustomTestMethod]
        public void GetByUserId_WithValidUserId_ShouldReturnEmployee()
        {
            var employeeService = _container.Resolve<IEmployeeService>();
            var authService = _container.Resolve<IAuthService>();
            var authResult = authService.EmployeeRegister(employeeForRegisterDto);
            int userId = int.Parse(authResult.Data.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = employeeService.GetByUserId(userId);
            Assert.AreEqual(userId, result.UserId);
        }
    }
}
