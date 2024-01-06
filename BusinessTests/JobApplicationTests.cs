namespace BusinessTests
{
    [TestClass]
    public class JobApplicationTests: TestBase
    {
        [CustomTestMethod]
        public void Apply_ShouldReturnSuccessResult_WhenJobApplicationIsSuccessful()
        {
            // Arrange
            var jobApplicationManager = _container.Resolve<IJobApplicationService>();
            // Act
            var result = jobApplicationManager.Apply(0, 0);
            // Assert
            Assert.AreEqual(result.Message, Messages.ApplicationSuccessful);
        }
    }
}
