using AutoRental.Business.Entities;
using NP.Core.Business.Contracts;
using System;
using System.Collections.Generic;

namespace AutoRental.Business.Common
{
   public interface IAutoSalesEngine : IBusinessEngine
   {
      bool IsCarCurrentlySold(int carId, int accountId);
      bool IsCarCurrentlySold(int carId);
      bool IsCarAvailableForSale(int carId, DateTime pickupDate, DateTime returnDate,
           IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars, IEnumerable<Sale> soldCars);
      Sale SellCarToCustomer(string loginEmail, int carId, DateTime saleDate, DateTime dateSaleFinal);
   }
}
