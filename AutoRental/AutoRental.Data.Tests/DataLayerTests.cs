using AutoRental.Business.Bootstrapper;
using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using NP.Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutoRental.Data.Tests
{
   [TestClass]
   public class DataLayerTests
   {
      #region Initialize

      [TestInitialize]
      public void Initialize()
      {
         AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
         ObjectBase.Container = MEFLoader.Init();
      }
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Methods

      [TestMethod]
      public void test_repository_usage()
      {
         RepositoryTestClass repositoryTest = new RepositoryTestClass();

         IEnumerable<Car> cars = repositoryTest.GetCars();

         Assert.IsTrue(cars != null);
      }

      [TestMethod]
      public void test_repository_factory_usage()
      {
         RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

         IEnumerable<Car> cars = factoryTest.GetCars();

         Assert.IsTrue(cars != null);
      }

      [TestMethod]
      public void test_repository_mocking()
      {
         List<Car> cars = new List<Car>()
         {
            new Car() { CarId = 1, Description = "Mustang" },
            new Car() { CarId = 2, Description = "Corvette" }
         };

         Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
         mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

         RepositoryTestClass repositoryTest = new RepositoryTestClass(mockCarRepository.Object);

         IEnumerable<Car> ret = repositoryTest.GetCars();

         Assert.IsTrue(ret == cars);
      }

      [TestMethod]
      public void test_factory_mocking1()
      {
         List<Car> cars = new List<Car>()
         {
            new Car() { CarId = 1, Description = "Mustang" },
            new Car() { CarId = 2, Description = "Corvette" }
         };

         Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
         mockDataRepository.Setup(obj => obj.GetDataRepository<ICarRepository>().Get()).Returns(cars);

         RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

         IEnumerable<Car> ret = factoryTest.GetCars();

         Assert.IsTrue(ret == cars);
      }

      [TestMethod]
      public void test_factory_mocking2()
      {
         List<Car> cars = new List<Car>()
         {
            new Car() { CarId = 1, Description = "Mustang" },
            new Car() { CarId = 2, Description = "Corvette" }
         };

         Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
         mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

         Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
         mockDataRepository.Setup(obj => obj.GetDataRepository<ICarRepository>()).Returns(mockCarRepository.Object);

         RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

         IEnumerable<Car> ret = factoryTest.GetCars();

         Assert.IsTrue(ret == cars);
      }
      #endregion
   }
}

