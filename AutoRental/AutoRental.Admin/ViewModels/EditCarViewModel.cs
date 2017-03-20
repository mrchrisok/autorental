using AutoRental.Admin.Support;
using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Core;
using NP.Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using NP.Core.Common.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Admin.ViewModels
{
   /// <summary>
   /// Instantiated directly on-demand by MaintainCars view. Is not injected via DI
   /// </summary>
   public class EditCarViewModel : ViewModelBase
   {
      #region Events

      public event EventHandler CancelEditCar;
      public event EventHandler<CarEventArgs> CarUpdated;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Declarations

      IServiceFactory _ServiceFactory;
      Car _Car;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Constructors

      public EditCarViewModel(IServiceFactory serviceFactory, Car car)
      {
         _ServiceFactory = serviceFactory;
         _Car = new Car()
         {
            CarId = car.CarId,
            Description = car.Description,
            Color = car.Color,
            Year = car.Year,
            RentalPrice = car.RentalPrice
         };

         _Car.CleanAll();

         SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
         CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Properties

      public DelegateCommand<object> SaveCommand { get; private set; }
      public DelegateCommand<object> CancelCommand { get; private set; }

      public Car Car
      {
         get { return _Car; }
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods

      protected override void AddModels(List<ObjectBase> models)
      {
         models.Add(Car);
      }

      void OnSaveCommandExecute(object arg)
      {
         ValidateModel();

         if (IsValid)
         {
            // call service via proxy to update the car
            WithClient<IInventoryService>(_ServiceFactory.CreateClient<IInventoryService>(), inventoryClient =>
            {
               bool isNew = (_Car.CarId == 0);

               var savedCar = inventoryClient.UpdateCar(_Car);
               if (savedCar != null)
               {
                  if (CarUpdated != null)
                     CarUpdated(this, new CarEventArgs(savedCar, isNew));
               }
            });
         }
      }

      bool OnSaveCommandCanExecute(object arg)
      {
         return _Car.IsDirty;
      }

      void OnCancelCommandExecute(object arg)
      {
         if (CancelEditCar != null)
            CancelEditCar(this, EventArgs.Empty);
      }
      #endregion
   }
}