using AutoRental.Business.Common;
using AutoRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AutoRental.Business
{
   [Export(typeof(IAutoSalesEngine))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class AutoSalesEngine : IAutoSalesEngine
   {
      #region Properties

      IDataRepositoryFactory _DataRepositoryFactory;
      #endregion

      //----------------------------------------------------------------------------------------------
      #region Constructors

      [ImportingConstructor]
      public AutoSalesEngine(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }
      #endregion

      //----------------------------------------------------------------------------------------------
      #region Methods

      public bool IsCarCurrentlySold(int carId, int accountId)
      {
         throw new NotImplementedException();
      }

      public bool IsCarCurrentlySold(int carId)
      {
         throw new NotImplementedException();
      }

      public bool IsCarAvailableForSale(int carId, DateTime pickupDate, DateTime returnDate, IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars, IEnumerable<Sale> soldCars)
      {
         throw new NotImplementedException();
      }

      public Sale SellCarToCustomer(string loginEmail, int carId, DateTime saleDate, DateTime dateSaleFinal)
      {
         throw new NotImplementedException();
      }
      #endregion
   }
}
