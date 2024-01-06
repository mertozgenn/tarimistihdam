namespace BusinessTests
{
    [TestClass]
    public class JobCategoryTests: TestBase
    {
        [CustomTestMethod]
        public void GetAll_ShouldGetAllJobCategories()
        {
            // Arrange
            var jobCategoryManager = _container.Resolve<IJobCategoryService>();
            // Act
            var result = jobCategoryManager.GetAll();
            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }
    }
}
