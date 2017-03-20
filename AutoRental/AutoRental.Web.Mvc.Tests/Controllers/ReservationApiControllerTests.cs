using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using AutoRental.Web.Mvc.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Web.Http;

namespace AutoRental.Web.Mvc.Tests.Controllers
{
    [TestClass]
    public class ReservationApiControllerTests
    {
        #region Properties

        HttpRequestMessage _Request = null;
        #endregion

        //-------------------------------------------------------------------------------------------------
        #region Initialize

        [TestInitialize]
        public void Initializer()
        {
            _Request = GetRequest();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------
        #region Methods

        [TestMethod]
        public void GetAvailableCars()
        {
            Mock<IInventoryService> mockInventoryService = new Mock<IInventoryService>();
            Mock<IRentalService> mockRentalService = new Mock<IRentalService>();

            Car[] cars = 
            {
                new Car() { CarId = 1 },
                new Car() { CarId = 2 }
            };

            mockInventoryService.Setup(obj => obj.GetAvailableCars(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(cars);

            ReservationApiController controller = new ReservationApiController(mockInventoryService.Object, mockRentalService.Object);

            HttpResponseMessage response = controller.GetAvailableCars(_Request, DateTime.Now, DateTime.Now.AddDays(1));
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

            Car[] data = GetResponseData<Car[]>(response);
            Assert.IsTrue(data == cars);
        }
        #endregion

        //-------------------------------------------------------------------------------------------------
        #region Helpers

        HttpRequestMessage GetRequest()
        {
            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties["MS_HttpConfiguration"] = config;
            return request;
        }

        T GetResponseData<T>(HttpResponseMessage result)
        {
            ObjectContent<T> content = result.Content as ObjectContent<T>;
            if (content != null)
            {
                T data = (T)(content.Value);
                return data;
            }
            else
                return default(T);
        }
        #endregion
    }
}

