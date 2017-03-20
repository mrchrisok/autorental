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
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;

namespace AutoRental.Business.Managers
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple,
                  ReleaseServiceInstanceOnTransactionComplete = false)]
   public class RentalManager : ManagerBase, IRentalService
   {
      #region Constructors

      public RentalManager() { }

      public RentalManager(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }

      public RentalManager(IBusinessEngineFactory businessEngineFactory)
      {
         _BusinessEngineFactory = businessEngineFactory;
      }

      public RentalManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
         _BusinessEngineFactory = businessEngineFactory;
      }
      #endregion
      //----------------------------------------------------------------------------------------

      #region Operations.IRentalService

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            try
            {
               Rental rental = AutoRentalEngine.RentCarToCustomer(loginEmail, carId, DateTime.Now, dateDueBack);
               return rental;
            }
            catch (UnableToRentForDateException ex)
            {
               throw new FaultException<UnableToRentForDateException>(ex, ex.Message);
            }
            catch (CarCurrentlyRentedException ex)
            {
               throw new FaultException<CarCurrentlyRentedException>(ex, ex.Message);
            }
            catch (NotFoundException ex)
            {
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            try
            {
               Rental rental = AutoRentalEngine.RentCarToCustomer(loginEmail, carId, rentalDate, dateDueBack);
               return rental;
            }
            catch (UnableToRentForDateException ex)
            {
               throw new FaultException<UnableToRentForDateException>(ex, ex.Message);
            }
            catch (CarCurrentlyRentedException ex)
            {
               throw new FaultException<CarCurrentlyRentedException>(ex, ex.Message);
            }
            catch (NotFoundException ex)
            {
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public void AcceptCarReturn(int carId)
      {
         ExecuteFaultHandledOperation(() =>
         {
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            Rental rental = rentalRepository.GetCurrentRentalByCar(carId);
            if (rental == null)
            {
               CarNotRentedException ex = new CarNotRentedException(string.Format("Car {0} is not currently rented.", carId));
               throw new FaultException<CarNotRentedException>(ex, ex.Message);
            }

            rental.DateReturned = DateTime.Now;

            Rental updatedRentalEntity = rentalRepository.Update(rental);
         });
      }


      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public IEnumerable<Rental> GetRentalHistory(string loginEmail)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(account);

            IEnumerable<Rental> rentalHistory = rentalRepository.GetRentalHistoryByAccount(account.AccountId);

            return rentalHistory;
         });
      }

      [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Reservation GetReservation(int reservationId)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            Reservation reservation = reservationRepository.Get(reservationId);
            if (reservation == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No reservation found for id '{0}'.", reservationId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(reservation);

            return reservation;
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(account);

            Reservation reservation = new Reservation()
            {
               AccountId = account.AccountId,
               CarId = carId,
               RentalDate = rentalDate,
               ReturnDate = returnDate
            };

            Reservation savedEntity = reservationRepository.Add(reservation);

            return savedEntity;
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public void ExecuteRentalFromReservation(int reservationId)
      {
         ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            Reservation reservation = reservationRepository.Get(reservationId);
            if (reservation == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("Reservation {0} is not found.", reservationId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            Account account = accountRepository.Get(reservation.AccountId);
            if (account == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No account found for account ID '{0}'.", reservation.AccountId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            try
            {
               Rental rental = AutoRentalEngine.RentCarToCustomer(account.LoginEmail, reservation.CarId, reservation.RentalDate, reservation.ReturnDate);
            }
            catch (UnableToRentForDateException ex)
            {
               throw new FaultException<UnableToRentForDateException>(ex, ex.Message);
            }
            catch (CarCurrentlyRentedException ex)
            {
               throw new FaultException<CarCurrentlyRentedException>(ex, ex.Message);
            }
            catch (NotFoundException ex)
            {
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            reservationRepository.Remove(reservation);
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public void CancelReservation(int reservationId)
      {
         ExecuteFaultHandledOperation(() =>
         {
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            Reservation reservation = reservationRepository.Get(reservationId);
            if (reservation == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No reservation found found for ID '{0}'.", reservationId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(reservation);

            reservationRepository.Remove(reservationId);
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public CustomerReservationData[] GetCurrentReservations()
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

            IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCurrentCustomerReservationInfo();
            foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
            {
               reservationData.Add(new CustomerReservationData()
               {
                  ReservationId = reservationInfo.Reservation.ReservationId,
                  Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                  CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                  RentalDate = reservationInfo.Reservation.RentalDate,
                  ReturnDate = reservationInfo.Reservation.ReturnDate
               });
            }

            return reservationData.ToArray();
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public CustomerReservationData[] GetCustomerReservations(string loginEmail)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(account);

            List<CustomerReservationData> reservationData = new List<CustomerReservationData>();

            IEnumerable<CustomerReservationInfo> reservationInfoSet = reservationRepository.GetCustomerOpenReservationInfo(account.AccountId);
            foreach (CustomerReservationInfo reservationInfo in reservationInfoSet)
            {
               reservationData.Add(new CustomerReservationData()
               {
                  ReservationId = reservationInfo.Reservation.ReservationId,
                  Car = reservationInfo.Car.Color + " " + reservationInfo.Car.Year + " " + reservationInfo.Car.Description,
                  CustomerName = reservationInfo.Customer.FirstName + " " + reservationInfo.Customer.LastName,
                  RentalDate = reservationInfo.Reservation.RentalDate,
                  ReturnDate = reservationInfo.Reservation.ReturnDate
               });
            }

            return reservationData.ToArray();
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Rental GetRental(int rentalId)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Rental rental = rentalRepository.Get(rentalId);
            if (rental == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No rental record found for id '{0}'.", rentalId));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(rental);

            return rental;
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public CustomerRentalData[] GetCurrentRentals()
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            List<CustomerRentalData> rentalData = new List<CustomerRentalData>();

            IEnumerable<CustomerRentalInfo> rentalInfoSet = rentalRepository.GetCurrentCustomerRentalInfo();
            foreach (CustomerRentalInfo rentalInfo in rentalInfoSet)
            {
               rentalData.Add(new CustomerRentalData()
               {
                  RentalId = rentalInfo.Rental.RentalId,
                  Car = rentalInfo.Car.Color + " " + rentalInfo.Car.Year + " " + rentalInfo.Car.Description,
                  CustomerName = rentalInfo.Customer.FirstName + " " + rentalInfo.Customer.LastName,
                  DateRented = rentalInfo.Rental.DateRented,
                  ExpectedReturn = rentalInfo.Rental.DateDue
               });
            }

            return rentalData.ToArray();
         });
      }


      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public CustomerRentalData[] GetCustomerRentals(string loginEmail)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
            {
               NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));
               throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            ValidateAuthorization(account);

            List<CustomerRentalData> rentalData = new List<CustomerRentalData>();

            IEnumerable<CustomerRentalInfo> rentalInfoSet = rentalRepository.GetCustomerRentalInfoByAccount(account.AccountId);
            foreach (CustomerRentalInfo rentalInfo in rentalInfoSet)
            {
               rentalData.Add(new CustomerRentalData()
               {
                  RentalId = rentalInfo.Rental.RentalId,
                  CustomerName = rentalInfo.Customer.FirstName + " " + rentalInfo.Customer.LastName,
                  Car = rentalInfo.Car.Color + " " + rentalInfo.Car.Year + " " + rentalInfo.Car.Description,
                  DateRented = rentalInfo.Rental.DateRented,
                  ExpectedReturn = rentalInfo.Rental.DateDue,
                  DateReturned = rentalInfo.Rental.DateReturned
               });
            }

            return rentalData.ToArray();
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public Reservation[] GetDeadReservations()
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

            IEnumerable<Reservation> reservations = reservationRepository.GetReservationsByPickupDate(DateTime.Now.AddDays(-1));

            return (reservations != null ? reservations.ToArray() : null);
         });
      }

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      public bool IsCarCurrentlyRented(int carId)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            IAutoRentalEngine AutoRentalEngine = _BusinessEngineFactory.GetBusinessEngine<IAutoRentalEngine>();

            return AutoRentalEngine.IsCarCurrentlyRented(carId);
         });
      }
      #endregion
   }
}
