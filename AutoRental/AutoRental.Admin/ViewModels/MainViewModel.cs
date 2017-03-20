using NP.Core.Common.UI.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Admin.ViewModels
{
   [Export]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class MainViewModel : ViewModelBase
   {
      //------------------------------------------------------------------------------
      #region Accessors

      [Import]
      public DashboardViewModel DashboardViewModel { get; private set; }

      [Import]
      public MaintainCarsViewModel MaintainCarsViewModel { get; private set; }

      [Import]
      public ReservationsViewModel ReservationsViewModel { get; private set; }

      [Import]
      public RentalsViewModel RentalsViewModel { get; private set; }
      #endregion
   }
}
