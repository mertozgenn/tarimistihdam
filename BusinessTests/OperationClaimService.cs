namespace BusinessTests
{
    [TestClass]
    public class OperationClaimService: TestBase
    {
        [CustomTestMethod]
        public void GetEmployee_ShouldGetEmployeeOperationClaim()
        {
            // Arrange
            var operationClaimManager = _container.Resolve<IOperationClaimService>();
            // Act
            var result = operationClaimManager.GetEmployee();
            // Assert
            Assert.IsNotNull(result);
        }

        [CustomTestMethod]
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
