using Entities.Dtos.Job;

namespace BusinessTests
{
    [TestClass]
    public class JobTests: TestBase
    {
        JobToAddDto jobToAddDto = new JobToAddDto
        {
            CategoryId = 0,
            CityId = 0,
            DailyWage = 10,
            Description = "Merhaba manisadaki üzüm tarlam için 20 adet tarım işçisi arıyorum.",
            DistrictId = 0,
            EmployerId = 0,
            EmployeeCount = 0,
            Image = null,
            Title = "Üzüm tarlama işçi arıyorum"
        };

        [TestMethod]
        public void GetAll_ShouldGetAllJobs()
        {
            // Arrange
            var jobManager = _container.Resolve<IJobService>();
            jobManager.Add(jobToAddDto);
            // Act
            var result = jobManager.GetAll();
            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void GetByIds_ShouldGetJobsByIds()
        {
            // Arrange
            var jobManager = _container.Resolve<IJobService>();
            // Act
            var result = jobManager.GetByIds(new List<int> { 0 });
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_ShouldAddJob()
        {
            // Arrange
            var jobManager = _container.Resolve<IJobService>();
            // Act
            var result = jobManager.Add(jobToAddDto);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void GetByEmployerId_ShouldGetJobsByEmployerId()
        {
            // Arrange
            var jobManager = _container.Resolve<IJobService>();
            // Act
            var result = jobManager.GetByEmployerId(0);
            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var jobDal = _container.Resolve<IJobDal>();
            var jobs = jobDal.GetAll(x => x.CategoryId == 0);
            jobDal.DeleteAll(jobs);
        }
    }
}
