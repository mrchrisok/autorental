using AutoRental.Business.Entities;
using NP.Core.Business.Contracts;
using System;
using System.Collections.Generic;

namespace AutoRental.Business.Common
{
   public interface IAutoRentalEngine : IBusinessEngine
   {
      bool IsCarCurrentlyRented(int carId, int accountId);
      bool IsCarCurrentlyRented(int carId);
      bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate,
                                    IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars);
      Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);
   }
}
