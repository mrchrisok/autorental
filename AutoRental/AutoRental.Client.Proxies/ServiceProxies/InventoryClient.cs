using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using NP.Core.Common.ServiceModel;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace AutoRental.Client.Proxies
{
   [Export(typeof(IInventoryService))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
   {
      //----------------------------------------------------------------------------
      #region Operations

      public Car UpdateCar(Car car)
      {
         return Channel.UpdateCar(car);
      }

      public void DeleteCar(int carId)
      {
         Channel.DeleteCar(carId);
      }

      public Car GetCar(int carId)
      {
         return Channel.GetCar(carId);
      }

      public Car[] GetAllCars()
      {
         return Channel.GetAllCars();
      }

      public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
      {
         return Channel.GetAvailableCars(pickupDate, returnDate);
      }
      #endregion


      //----------------------------------------------------------------------------
      #region Operations.Async

      public Task<Car> UpdateCarAsync(Car car)
      {
         return Channel.UpdateCarAsync(car);
      }

      public Task DeleteCarAsync(int carId)
      {
         return Channel.DeleteCarAsync(carId);
      }

      public Task<Car> GetCarAsync(int carId)
      {
         return Channel.GetCarAsync(carId);
      }

      public Task<Car[]> GetAllCarsAsync()
      {
         return Channel.GetAllCarsAsync();
      }

      public Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate)
      {
         return Channel.GetAvailableCarsAsync(pickupDate, returnDate);
      }
      #endregion
   }
}
