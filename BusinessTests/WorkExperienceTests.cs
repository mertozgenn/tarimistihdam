using Entities.Dtos.User;
using Entities.Dtos.WorkExperience;

namespace BusinessTests
{
    [TestClass]
    public class WorkExperienceTests: TestBase
    {
        EmployeeForRegisterDto _employeeForRegisterDto = new()
        {
            Email = "test@test.com",
            Name = "Test",
            Surname = "Test",
            Password = "Test",
            Phone = "Test",
            Tckn = "Test"
        };

        [TestMethod]
        public void Add_WithValidData_ShoulAddWorkExperience()
        {
            //Arrange
            var workExperienceService = _container.Resolve<IWorkExperienceService>();
            WorkExperienceToAddDto workExperienceToAddDto = new()
            {
                Title = "Test",
                Description = "Test",
                EmployeeId = 0
            };
            //Act
            var result = workExperienceService.Add(workExperienceToAddDto);
            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_ShouldDeleteWorkExperience()
        {
            //Arrange
            var workExperienceService = _container.Resolve<IWorkExperienceService>();
            WorkExperienceToAddDto workExperienceToAddDto = new()
            {
                Title = "Test",
                Description = "Test",
                EmployeeId = 0
            };
            workExperienceService.Add(workExperienceToAddDto);
            var workExperienceDal = _container.Resolve<IWorkExperienceDal>();
            var workExperience = workExperienceDal.Get(x => x.EmployeeId == 0);
            //Act
            var result = workExperienceService.Delete(workExperience.Id, 0);
            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var workExperienceDal = _container.Resolve<IWorkExperienceDal>();
            workExperienceDal.DeleteAll(workExperienceDal.GetAll(x => x.EmployeeId == 0));
        }
    }
}
