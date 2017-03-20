using AutoRental.Admin.ViewModels;
using Core.Common.Misc;
using NP.Core.Common.UI.Core;
using System;
using System.Windows;

namespace AutoRental.Admin.Views
{
   public partial class RentalsView : UserControlViewBase
   {
      //------------------------------------------------------------------------------------
      #region Constructors

      public RentalsView()
      {
         InitializeComponent();
      }
      #endregion

      //------------------------------------------------------------------------------------
      #region Methods

      protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
      {
         RentalsViewModel vm = viewModel as RentalsViewModel;
         if (vm != null)
         {
            vm.RentalReturned -= OnRentalReturned;
            vm.ErrorOccured -= OnErrorOccured;
         }
      }

      protected override void OnWireViewModelEvents(ViewModelBase viewModel)
      {
         RentalsViewModel vm = viewModel as RentalsViewModel;
         if (vm != null)
         {
            vm.RentalReturned += OnRentalReturned;
            vm.ErrorOccured += OnErrorOccured;
         }
      }

      void OnRentalReturned(object sender, EventArgs e)
      {
         MessageBox.Show("Rental returned.");
      }

      void OnErrorOccured(object sender, ErrorMessageEventArgs e)
      {
         MessageBox.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      #endregion
   }
}
