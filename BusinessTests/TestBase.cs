using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessTests
{
    [TestClass]
    public abstract class TestBase
    {
        internal IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacBusinessModule());
            _container = builder.Build();
        }
    }
}