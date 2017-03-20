using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using Core.Common.Contracts;
using NP.Core.Common.UI.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Admin.ViewModels
{
   [Export]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class DashboardViewModel : ViewModelBase
   {
      //----------------------------------------------------------------------------------
      #region Properties

      IServiceFactory _ServiceFactory;
      Car[] _Cars;
      //CustomerRentalData[] _CurrentlyRented;
      #endregion

      //----------------------------------------------------------------------------------
      #region Constructors

      [ImportingConstructor]
      public DashboardViewModel(IServiceFactory serviceFactory)
      {
         _ServiceFactory = serviceFactory;
      }
      #endregion

      //----------------------------------------------------------------------------------
      #region Accessors

      public override string ViewTitle
      {
         get { return "Dashboard"; }
      }

      public Car[] Cars
      {
         get { return _Cars; }
         set
         {
            if (_Cars != value)
            {
               _Cars = value;
               OnPropertyChanged(() => Cars, false);
            }
         }
      }
      #endregion

      //----------------------------------------------------------------------------------
      #region Methods

      //--// Overrides
      protected override void OnViewLoaded()
      {
         // can check properties for null here if not want to re-get every time view shows

         WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), inventoryClient =>
         {
            Cars = inventoryClient.GetAllCars();
         });
      }
      #endregion
   }
}
