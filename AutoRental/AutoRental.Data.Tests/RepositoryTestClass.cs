using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using NP.Core.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AutoRental.Data.Tests
{
   public class RepositoryTestClass
   {
      #region Properties

      [Import]
      ICarRepository _CarRepository;
      #endregion

      //------------------------------------------------------------------------------------------------
      #region Constructors

      public RepositoryTestClass()
      {
         ObjectBase.Container.SatisfyImportsOnce(this);
      }

      public RepositoryTestClass(ICarRepository carRepository)
      {
         _CarRepository = carRepository;
      }
      #endregion

      //------------------------------------------------------------------------------------------------
      #region Methods

      public IEnumerable<Car> GetCars()
      {
         IEnumerable<Car> cars = _CarRepository.Get();

         return cars;
      }
      #endregion
   }
}
