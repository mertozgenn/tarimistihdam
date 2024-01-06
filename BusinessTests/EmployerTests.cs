using Entities.Dtos.User;
using System.Security.Claims;

namespace BusinessTests
{
    [TestClass]
    public class EmployerTests: TestBase 
    {
        EmployerForRegisterDto employerForRegisterDto = new EmployerForRegisterDto
        {
            CompanyName = "Test",
            Email = "test@₺est.com",
            Name = "Test",
            Password = "123",
            Phone = "1234567890",
            Surname = "Test",
            TaxNumber = "12345678901",
            TaxPlace = "Test",
            Tckn = "12345678901",
            RePassword = "123"
        };

        [CustomTestMethod]
        public void GetByUserId_WithValidUserId_ShouldReturnEmployer()
        {
            var employerService = _container.Resolve<IEmployerService>();
            var authService = _container.Resolve<IAuthService>();
            var authResult = authService.EmployerRegister(employerForRegisterDto);
            int userId = int.Parse(authResult.Data.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = employerService.GetByUserId(userId);
            Assert.AreEqual(userId, result.UserId);
        }
    }
}
