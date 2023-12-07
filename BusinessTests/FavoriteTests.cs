using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTests
{
    [TestClass]
    public class FavoriteTests: TestBase
    {
        [TestMethod]
        public void Add_ShouldAddFavorite()
        {
            // Arrange
            var favoriteManager = _container.Resolve<IFavoriteService>();
            // Act
            var result = favoriteManager.Add(0, 0);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_ShouldDeleteFavorite()
        {
            // Arrange
            var favoriteManager = _container.Resolve<IFavoriteService>();
            favoriteManager.Add(0, 0);
            var favoriteDal = _container.Resolve<IFavoriteDal>();
            var data = favoriteDal.Get(x => x.JobId == 0 && x.EmployeeId == 0);
            // Act
            var result = favoriteManager.Delete(data.Id, 0);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void GetFavorites_ShouldGetFavorites()
        {
            // Arrange
            var favoriteManager = _container.Resolve<IFavoriteService>();
            favoriteManager.Add(0, 0);
            // Act
            var result = favoriteManager.GetFavorites(0);
            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var favoriteDal = _container.Resolve<IFavoriteDal>();
            var data = favoriteDal.GetAll(x => x.EmployeeId == 0);
            favoriteDal.DeleteAll(data);
        }
    }
}
