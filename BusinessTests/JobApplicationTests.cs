namespace BusinessTests
{
    [TestClass]
    public class JobApplicationTests: TestBase
    {
        [TestMethod]
        public void Apply_ShouldReturnSuccessResult_WhenJobApplicationIsSuccessful()
        {
            // Arrange
            var jobApplicationManager = _container.Resolve<IJobApplicationService>();
            // Act
            var result = jobApplicationManager.Apply(0, 0);
            // Assert
            Assert.AreEqual(result.Message, Messages.ApplicationSuccessful);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var jobApplicationDal = _container.Resolve<IJobApplicationDal>();
            jobApplicationDal.DeleteAll(jobApplicationDal.GetAll(x => x.EmployeeId == 0));
        }
    }
}
