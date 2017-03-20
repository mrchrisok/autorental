using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using NP.Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace AutoRental.Client.Proxies
{
   [Export(typeof(IRentalService))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class RentalClient : UserClientBase<IRentalService>, IRentalService
   {
      //----------------------------------------------------------------------------------------
      #region Operations

      public Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack)
      {
         return Channel.RentCarToCustomer(loginEmail, carId, dateDueBack);
      }

      public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
      {
         return Channel.RentCarToCustomer(loginEmail, carId, rentalDate, dateDueBack);
      }

      public void AcceptCarReturn(int carId)
      {
         Channel.AcceptCarReturn(carId);
      }

      public IEnumerable<Rental> GetRentalHistory(string loginEmail)
      {
         return Channel.GetRentalHistory(loginEmail);
      }

      public Reservation GetReservation(int reservationId)
      {
         return Channel.GetReservation(reservationId);
      }

      public Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
      {
         return Channel.MakeReservation(loginEmail, carId, rentalDate, returnDate);
      }

      public void ExecuteRentalFromReservation(int reservationId)
      {
         Channel.ExecuteRentalFromReservation(reservationId);
      }

      public void CancelReservation(int reservationId)
      {
         Channel.CancelReservation(reservationId);
      }

      public CustomerReservationData[] GetCurrentReservations()
      {
         return Channel.GetCurrentReservations();
      }

      public CustomerReservationData[] GetCustomerReservations(string loginEmail)
      {
         return Channel.GetCustomerReservations(loginEmail);
      }

      public CustomerRentalData[] GetCustomerRentals(string loginEmail)
      {
         return Channel.GetCustomerRentals(loginEmail);
      }

      public Rental GetRental(int rentalId)
      {
         return Channel.GetRental(rentalId);
      }

      public CustomerRentalData[] GetCurrentRentals()
      {
         return Channel.GetCurrentRentals();
      }

      public Reservation[] GetDeadReservations()
      {
         return Channel.GetDeadReservations();
      }

      public bool IsCarCurrentlyRented(int carId)
      {
         return Channel.IsCarCurrentlyRented(carId);
      }
      #endregion

      //----------------------------------------------------------------------------------------
      #region Operations.Async

      public Task<Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime dateDueBack)
      {
         return Channel.RentCarToCustomerAsync(loginEmail, carId, dateDueBack);
      }

      public Task<Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
      {
         return Channel.RentCarToCustomerAsync(loginEmail, carId, rentalDate, dateDueBack);
      }

      public Task AcceptCarReturnAsync(int carId)
      {
         return Channel.AcceptCarReturnAsync(carId);
      }

      public Task<IEnumerable<Rental>> GetRentalHistoryAsync(string loginEmail)
      {
         return Channel.GetRentalHistoryAsync(loginEmail);
      }

      public Task<Reservation> GetReservationAsync(int reservationId)
      {
         return Channel.GetReservationAsync(reservationId);
      }

      public Task<Reservation> MakeReservationAsync(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
      {
         return Channel.MakeReservationAsync(loginEmail, carId, rentalDate, returnDate);
      }

      public Task ExecuteRentalFromReservationAsync(int reservationId)
      {
         return Channel.ExecuteRentalFromReservationAsync(reservationId);
      }

      public Task CancelReservationAsync(int reservationId)
      {
         return Channel.CancelReservationAsync(reservationId);
      }

      public Task<CustomerReservationData[]> GetCurrentReservationsAsync()
      {
         return Channel.GetCurrentReservationsAsync();
      }

      public Task<CustomerReservationData[]> GetCustomerReservationsAsync(string loginEmail)
      {
         return Channel.GetCustomerReservationsAsync(loginEmail);
      }

      public Task<CustomerRentalData[]> GetCustomerRentalsAsync(string loginEmail)
      {
         return Channel.GetCustomerRentalsAsync(loginEmail);
      }

      public Task<Rental> GetRentalAsync(int rentalId)
      {
         return Channel.GetRentalAsync(rentalId);
      }

      public Task<CustomerRentalData[]> GetCurrentRentalsAsync()
      {
         return Channel.GetCurrentRentalsAsync();
      }

      public Task<Reservation[]> GetDeadReservationsAsync()
      {
         return Channel.GetDeadReservationsAsync();
      }

      public Task<bool> IsCarCurrentlyRentedAsync(int carId)
      {
         return Channel.IsCarCurrentlyRentedAsync(carId);
      }
      #endregion
   }
}
