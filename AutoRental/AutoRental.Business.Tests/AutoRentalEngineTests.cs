using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoRental.Business.Tests
{
   [TestClass]
   public class AutoRentalEngineTests
   {
      [TestMethod]
      public void IsCarCurrentlyRented_any_account()
      {
         Rental rental = new Rental()
         {
            CarId = 1
         };

         // set up mock rental repository
         Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
         mockRentalRepository.Setup(obj => obj.GetCurrentRentalByCar(1)).Returns(rental);

         // set up mock data repository factory
         Mock<IDataRepositoryFactory> mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
         mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

         AutoRentalEngine engine = new AutoRentalEngine(mockRepositoryFactory.Object);

         bool try1 = engine.IsCarCurrentlyRented(2);

         Assert.IsFalse(try1);

         bool try2 = engine.IsCarCurrentlyRented(1);

         Assert.IsTrue(try2);
      }
   }
}
