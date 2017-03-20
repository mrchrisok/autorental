using AutoRental.Client.Contracts;
using Core.Common;
using Core.Common.Contracts;
using NP.Core.Common.Extensions;
using NP.Core.Common.UI.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Misc;
using AutoRental.Admin.Support;
using AutoRental.Common;
using System.ServiceModel;

namespace AutoRental.Admin.ViewModels
{
   [Export]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class ReservationsViewModel : ViewModelBase
   {
      #region Events

      public event EventHandler RentalExecuted;
      public event EventHandler ReservationCanceled;
      public event EventHandler<ErrorMessageEventArgs> RentalDenied;
      public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Properties

      IServiceFactory _ServiceFactory;
      ObservableCollection<CustomerReservationData> _Reservations;
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Accessors

      public DelegateCommand<int> ExecuteRentalCommand { get; private set; }
      public DelegateCommand<int> CancelReservationCommand { get; private set; }

      public override string ViewTitle
      {
         get { return "Current Reservations"; }
      }

      public ObservableCollection<CustomerReservationData> Reservations
      {
         get { return _Reservations; }
         set
         {
            if (_Reservations != value)
            {
               _Reservations = value;
               OnPropertyChanged(() => Reservations, false);
            }
         }
      }
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Constructors

      [ImportingConstructor]
      public ReservationsViewModel(IServiceFactory serviceFactory)
      {
         _ServiceFactory = serviceFactory;

         ExecuteRentalCommand = new DelegateCommand<int>(OnExecuteRentalCommandExecute);
         CancelReservationCommand = new DelegateCommand<int>(OnCancelReservationCommandExecute);
      }
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Methods

      //--// Overrides
      protected override void OnViewLoaded()
      {
         _Reservations = new ObservableCollection<CustomerReservationData>();

         WithClient<IRentalService>(_ServiceFactory.CreateClient<IRentalService>(), rentalClient =>
         {
            CustomerReservationData[] reservations = rentalClient.GetCurrentReservations();
            if (reservations != null)
            {
               // convert returned data into observable collection so binding can refresh automatically
               Reservations.Merge(reservations);
            }
         });
      }

      void OnExecuteRentalCommandExecute(int reservationId)
      {
         CustomerReservationData customerReservation = _Reservations.Where(item => item.ReservationId == reservationId).FirstOrDefault();
         //if (customerReservation.RentalDate > DateTime.Now) 
         //{
         //   RentalDenied(this, new RentalDeniedEventArgs("Rental not allowed prior to pickup date."));
         //}
         //else
         //{
            WithClient<IRentalService>(_ServiceFactory.CreateClient<IRentalService>(), rentalClient =>
            {
               if (customerReservation != null)
               {
                  try
                  {
                     rentalClient.ExecuteRentalFromReservation(reservationId);
                     Reservations.Remove(customerReservation);

                     if (RentalExecuted != null)
                        RentalExecuted(this, EventArgs.Empty);
                  }
                  catch (FaultException<UnableToRentForDateException> ex)
                  {
                     if (RentalDenied != null)
                        RentalDenied(this, new ErrorMessageEventArgs(ex.Message));
                  }
                  catch (FaultException ex)
                  {
                     if (RentalDenied != null)
                        RentalDenied(this, new ErrorMessageEventArgs(ex.Message));
                  }
                  catch (Exception ex)
                  {
                     if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                  }
               }
            });
         //}
      }

      void OnCancelReservationCommandExecute(int reservationId)
      {
         WithClient<IRentalService>(_ServiceFactory.CreateClient<IRentalService>(), rentalClient =>
         {
            CustomerReservationData customerReservation = _Reservations.Where(item => item.ReservationId == reservationId).FirstOrDefault();
            if (customerReservation != null)
            {
               try
               {
                  rentalClient.CancelReservation(reservationId);
                  Reservations.Remove(customerReservation);

                  if (ReservationCanceled != null)
                     ReservationCanceled(this, EventArgs.Empty);
               }
               catch (Exception ex)
               {
                  if (ErrorOccured != null)
                     ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
               }
            }
         });
      }
      #endregion
   }
}

