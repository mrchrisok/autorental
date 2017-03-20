using AutoRental.Common.Contracts;
using AutoRental.Web.Mvc.Controllers;
using AutoRental.Web.Mvc.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        //-------------------------------------------------------------------------------------------------
        #region Methods

        [TestMethod]
        public void Login()
        {
            Mock<ISecurityAdapter> mockSecurityAdapter = new Mock<ISecurityAdapter>();
            mockSecurityAdapter.Setup(obj => obj.Initialize());

            string returnUrl = "/testreturnurl";

            AccountController controller = new AccountController(mockSecurityAdapter.Object);

            ActionResult result = controller.Login(returnUrl);
            Assert.IsTrue(result is ViewResult);

            ViewResult viewResult = result as ViewResult;
            Assert.IsTrue(viewResult.Model is AccountLoginModel);

            AccountLoginModel model = viewResult.Model as AccountLoginModel;
            Assert.IsTrue(model.ReturnUrl == returnUrl);
        }
        #endregion
    }
}