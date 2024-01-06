namespace BusinessTests
{
    [TestClass] 
    public class CityDistrictTests: TestBase
    {
        [CustomTestMethod]
        public void GetCities_ReturnsListOfCities()
        {
            var cityDistrictService = _container.Resolve<ICityDistrictService>();
            var result = cityDistrictService.GetCities();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [CustomTestMethod]
        public void GetDistricts_ReturnsListOfDistricts()
        {
            var cityDistrictService = _container.Resolve<ICityDistrictService>();
            var result = cityDistrictService.GetDistrictsByCityId(35);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }
    }
}
