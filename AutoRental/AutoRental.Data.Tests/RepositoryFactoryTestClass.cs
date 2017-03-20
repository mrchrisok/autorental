using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using NP.Core.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AutoRental.Data.Tests
{
   public class RepositoryFactoryTestClass
   {
      #region Properties

      [Import]
      IDataRepositoryFactory _DataRepositoryFactory;
      #endregion

      //------------------------------------------------------------------------------------------------
      #region Constructors

      public RepositoryFactoryTestClass()
      {
         ObjectBase.Container.SatisfyImportsOnce(this);
      }

      public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }
      #endregion

      //------------------------------------------------------------------------------------------------
      #region Methdods

      public IEnumerable<Car> GetCars()
      {
         ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

         IEnumerable<Car> cars = carRepository.Get();

         return cars;
      }
      #endregion
   }
}
