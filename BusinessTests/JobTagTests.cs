namespace BusinessTests
{
    [TestClass]
    public class JobTagTests: TestBase
    {
        [CustomTestMethod]
        public void GetJobTags_ShouldReturnAllJobTags()
        {
            // Arrange
            var jobTagService = _container.Resolve<IJobTagService>();
            // Act
            var result = jobTagService.GetJobTags();
            // Assert
            Assert.IsNotNull(result);
        }

        [CustomTestMethod]
        public void GetJobTagsByIds_ShouldReturnJobTagsByIds()
        {
            // Arrange
            var jobTagService = _container.Resolve<IJobTagService>();
            var ids = new List<int> { 1, 2, 3 };
            // Act
            var result = jobTagService.GetJobTagsByIds(ids);
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
