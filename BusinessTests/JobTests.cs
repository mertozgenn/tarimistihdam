namespace BusinessTests
{
    [TestClass]
    public class JobTests: TestBase
    {
        [TestMethod]
        public void GetAll_ShouldGetAllJobs()
        {
            // Arrange
            var jobManager = _container.Resolve<IJobService>();
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
    }
}
