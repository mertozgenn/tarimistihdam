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
        [CustomTestMethod]
        public void Add_ShouldAddFavorite()
        {
            // Arrange
            var favoriteManager = _container.Resolve<IFavoriteService>();
            // Act
            var result = favoriteManager.Add(0, 0);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [CustomTestMethod]
        public void Delete_ShouldDeleteFavorite()
        {
            // Arrange
            var favoriteManager = _container.Resolve<IFavoriteService>();
            favoriteManager.Add(0, 0);
            // Act
            var result = favoriteManager.Delete(0, 0);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [CustomTestMethod]
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
    }
}
