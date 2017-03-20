using AutoRental.Web.Mvc.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //-------------------------------------------------------------------------------------------------
        #region Methods

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(null, result.ViewBag.Message);
        }
        #endregion
    }
}