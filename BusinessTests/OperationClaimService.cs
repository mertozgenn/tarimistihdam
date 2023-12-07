namespace BusinessTests
{
    [TestClass]
    public class OperationClaimService: TestBase
    {
        [TestMethod]
        public void GetEmployee_ShouldGetEmployeeOperationClaim()
        {
            // Arrange
            var operationClaimManager = _container.Resolve<IOperationClaimService>();
            // Act
            var result = operationClaimManager.GetEmployee();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetEmployer_ShouldGetEmployerOperationClaim()
        {
            // Arrange
            var operationClaimManager = _container.Resolve<IOperationClaimService>();
            // Act
            var result = operationClaimManager.GetEmployer();
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
