using AutoRental.Admin.ViewModels;
using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AutoRental.Admin.Tests
{
    [TestClass]
    public class DashboardViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Car[] data = new List<Car>()
            {
                new Car(),
                new Car()
            }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IInventoryService>().GetAllCars()).Returns(data);

            DashboardViewModel viewModel = new DashboardViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Cars == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Cars != null && viewModel.Cars.Length == data.Length && viewModel.Cars[0] == data[0]);
        }
    }
}
