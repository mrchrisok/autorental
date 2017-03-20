using AutoRental.Admin.Support;
using AutoRental.Admin.ViewModels;
using Core.Common.Misc;
using NP.Core.Common.UI.Core;
using System;
using System.Windows;

namespace AutoRental.Admin.Views
{
   public partial class ReservationsView : UserControlViewBase
   {
      public ReservationsView()
      {
         InitializeComponent();
      }

      #region original code before the base-class refactoring

      //private void OnDataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
      //{
      //    if (e.NewValue == null)
      //    {
      //        if (e.OldValue != null)
      //        {
      //            // view going out of scope and view-model disconnected (but still around in the parent)
      //            // unwire events to allow view to dispose
      //            ReservationsViewModel viewModel = e.OldValue as ReservationsViewModel;
      //            if (viewModel != null)
      //            {
      //                viewModel.RentalExecuted -= OnRentalExecuted;
      //                viewModel.ReservationCanceled -= OnReservationCanceled;
      //                viewModel.ErrorOccured -= OnErrorOccured;
      //            }
      //        }
      //    }
      //    else
      //    {
      //        ReservationsViewModel viewModel = e.NewValue as ReservationsViewModel;
      //        if (viewModel != null)
      //        {
      //            viewModel.RentalExecuted += OnRentalExecuted;
      //            viewModel.ReservationCanceled += OnReservationCanceled;
      //            viewModel.ErrorOccured += OnErrorOccured;
      //        }
      //    }
      //}

      #endregion

      protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
      {
         ReservationsViewModel vm = viewModel as ReservationsViewModel;
         if (vm != null)
         {
            vm.RentalExecuted -= OnRentalExecuted;
            vm.RentalDenied -= OnRentalDenied;
            vm.ReservationCanceled -= OnReservationCanceled;
            vm.ErrorOccured -= OnErrorOccured;
         }
      }

      protected override void OnWireViewModelEvents(ViewModelBase viewModel)
      {
         ReservationsViewModel vm = viewModel as ReservationsViewModel;
         if (vm != null)
         {
            vm.RentalExecuted += OnRentalExecuted;
            vm.RentalDenied += OnRentalDenied;
            vm.ReservationCanceled += OnReservationCanceled;
            vm.ErrorOccured += OnErrorOccured;
         }
      }

      void OnRentalExecuted(object sender, EventArgs e)
      {
         MessageBox.Show("Rental executed.");
      }

      void OnRentalDenied(object sender, ErrorMessageEventArgs e)
      {
         MessageBox.Show("Rental denied. " + e.ErrorMessage);
      }

      void OnReservationCanceled(object sender, EventArgs e)
      {
         MessageBox.Show("Reservation canceled.");
      }

      void OnErrorOccured(object sender, ErrorMessageEventArgs e)
      {
         MessageBox.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
   }
}
