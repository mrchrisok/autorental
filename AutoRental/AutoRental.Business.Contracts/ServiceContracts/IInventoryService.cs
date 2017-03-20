using AutoRental.Business.Entities;
using Core.Common.Exceptions;
using System;
using System.ServiceModel;

namespace AutoRental.Business.Contracts
{
   [ServiceContract]
   public interface IInventoryService
   {
      #region Operations

      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)] // if a transaction already exists, join it
      Car UpdateCar(Car car);

      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)] 
      void DeleteCar(int carId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      Car GetCar(int carId);

      [OperationContract]
      Car[] GetAllCars();

      [OperationContract]
      Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate);
      #endregion
   }
}
