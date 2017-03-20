using AutoRental.Business.Entities;
using AutoRental.Common;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoRental.Business.Contracts
{
   [ServiceContract]
   public interface IRentalService
   {
      [OperationContract(Name = "RentCarToCustomerImmediately")]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(CarCurrentlyRentedException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [FaultContract(typeof(UnableToRentForDateException))]
      Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack);

      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(CarCurrentlyRentedException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [FaultContract(typeof(UnableToRentForDateException))]
      Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void AcceptCarReturn(int carId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      IEnumerable<Rental> GetRentalHistory(string loginEmail);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      Reservation GetReservation(int reservationId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [FaultContract(typeof(UnableToRentForDateException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void ExecuteRentalFromReservation(int reservationId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void CancelReservation(int reservationId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      CustomerReservationData[] GetCurrentReservations();

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      CustomerReservationData[] GetCustomerReservations(string loginEmail);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      Rental GetRental(int rentalId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      CustomerRentalData[] GetCurrentRentals();

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      CustomerRentalData[] GetCustomerRentals(string loginEmail);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      Reservation[] GetDeadReservations();

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      bool IsCarCurrentlyRented(int carId);
   }
}

