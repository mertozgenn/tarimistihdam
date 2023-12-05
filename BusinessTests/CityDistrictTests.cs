namespace BusinessTests
{
    [TestClass] 
    public class CityDistrictTests: TestBase
    {
        [TestMethod]
        public void GetCities_ReturnsListOfCities()
        {
            var cityDistrictService = _container.Resolve<ICityDistrictService>();
            var result = cityDistrictService.GetCities();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void GetDistricts_ReturnsListOfDistricts()
        {
            var cityDistrictService = _container.Resolve<ICityDistrictService>();
            var result = cityDistrictService.GetDistricts();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }
    }
}
