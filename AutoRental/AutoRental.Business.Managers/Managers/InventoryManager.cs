using AutoRental.Business.Common;
using AutoRental.Business.Contracts;
using AutoRental.Business.Entities;
using AutoRental.Common;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using NP.Core.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;

namespace AutoRental.Business.Managers
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple,
                  ReleaseServiceInstanceOnTransactionComplete = false)]
   public class InventoryManager : ManagerBase, IInventoryService
   {
      #region Constructors

      public InventoryManager() { }

      public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }

      public InventoryManager(IBusinessEngineFactory businessEngineFactory)
      {
         _BusinessEngineFactory = businessEngineFactory;
      }

      public InventoryManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
         _BusinessEngineFactory = businessEngineFactory;
      }
      #endregion

      //----------------------------------------------------------------------------------------
      #region Operations.IInventoryService

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public Car UpdateCar(Car car)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            
            Car updatedEntity = null;

            if (car.CarId == 0)
               updatedEntity = carRepository.Add(car);
            else
               updatedEntity = carRepository.Update(car);

            return updatedEntity;
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public void DeleteCar(int carId)
      {
         ExecuteFaultHandledOperation(() =>
         {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            carRepository.Remove(carId);
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Car GetCar(int carId)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            Car carEntity = carRepository.Get(carId);

            if (carEntity == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in database", carId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }
            return carEntity;
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Car[] GetAllCars()
      {
         return ExecuteFaultHandledOperation(() =>
         {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            IEnumerable<Car> cars = carRepository.Get();
            IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();

            foreach (Car car in cars)
            {
                // set CurrentlyRented boolean to true if the car is currently rented
               Rental rentedCar = rentedCars.Where(item => item.CarId == car.CarId).FirstOrDefault();
               car.CurrentlyRented = (rentedCar != null);
            }

            return cars.ToArray();
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            IEnumerable<Car> allCars = carRepository.Get();
            IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();
            IEnumerable<Reservation> reservedCars = reservationRepository.Get();

            List<Car> availableCars = new List<Car>();

            foreach (Car car in allCars)
            {
               if (AutoRentalEngine.IsCarAvailableForRental(car.CarId, pickupDate, returnDate, rentedCars, reservedCars))
                  availableCars.Add(car);
            }

            return availableCars.ToArray();
         });
      }
      #endregion
   }
}
