using AutoRental.Admin.ViewModels;
using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace AutoRental.Admin.Tests
{
    [TestClass]
    public class RentalsViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Car car1 = new Car() { CarId = 1, Description = "Ford Mustang" };
            Car car2 = new Car() { CarId = 2, Description = "Dodge Caliber" };

            CustomerRentalData[] data = new List<CustomerRentalData>()
                {
                    new CustomerRentalData() { RentalId = 1,  Car = "Ford Mustang", CustomerName = "Mike" },
                    new CustomerRentalData() { RentalId = 2,  Car = "Dodge Caliber", CustomerName = "Jeff" },
                }.ToArray();


            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IRentalService>().GetCurrentRentals()).Returns(data);

            RentalsViewModel viewModel = new RentalsViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Rentals == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Rentals != null && viewModel.Rentals.Count == data.Length && viewModel.Rentals[0] == data[0]);
        }
    }
}
