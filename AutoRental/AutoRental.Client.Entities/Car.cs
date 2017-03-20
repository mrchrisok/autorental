using NP.Core.Common.Core;
using FluentValidation;
using System;

/// oco: namespace is mapped to '..pluralsight..' namespace in AssemblyInfo.cs
///     matched namespaces is required for client entitites <-> business entities serialization
///     DataContract & DataMember attributes are not used to avoid visullay polluting the client entities
///     The namespace mapping is adequate to enable .Net to treat the entity & public members as a data contract + members.
namespace AutoRental.Client.Entities
{
   public class Car : ObjectBase
   {
      #region Properties

      int _CarId;
      string _Description;
      string _Color;
      int _Year;
      decimal _RentalPrice;
      bool _CurrentlyRented;
      #endregion

      //-----------------------------------------------------------------------------------------------------
      #region Accessors

      public int CarId
      {
         get { return _CarId; }
         set
         {
            if (_CarId != value)
            {
               _CarId = value;
               OnPropertyChanged(() => CarId);
            }
         }
      }

      public string Description
      {
         get { return _Description; }
         set
         {
            if (_Description != value)
            {
               _Description = value;
               OnPropertyChanged(() => Description);
            }
         }
      }

      public string Color
      {
         get { return _Color; }
         set
         {
            if (_Color != value)
            {
               _Color = value;
               OnPropertyChanged(() => Color);
            }
         }
      }

      public int Year
      {
         get { return _Year; }
         set
         {
            if (_Year != value)
            {
               _Year = value;
               OnPropertyChanged(() => Year);
            }
         }
      }

      public decimal RentalPrice
      {
         get { return _RentalPrice; }
         set
         {
            if (_RentalPrice != value)
            {
               _RentalPrice = value;
               OnPropertyChanged(() => RentalPrice);
            }
         }
      }

      public bool CurrentlyRented
      {
         get { return _CurrentlyRented; }
         set
         {
            if (_CurrentlyRented != value)
            {
               _CurrentlyRented = value;
               OnPropertyChanged(() => CurrentlyRented);
            }
         }
      }

      public string LongDescription
      {
         get
         {
            return string.Format("{0} {1} {2}", _Year, _Color, _Description);
         }
      }
      #endregion

      //-----------------------------------------------------------------------------------------------------
      #region Methods.Override

      protected override IValidator GetValidator()
      {
          return new CarValidator();
      }
      #endregion

      //-----------------------------------------------------------------------------------------------------
      #region Helpers

      class CarValidator : AbstractValidator<Car>
      {
         public CarValidator()
         {
            RuleFor(obj => obj.Description).NotEmpty();
            RuleFor(obj => obj.Color).NotEmpty();
            RuleFor(obj => obj.RentalPrice).GreaterThan(0);
            RuleFor(obj => obj.Year).GreaterThan(2000).LessThanOrEqualTo(DateTime.Now.Year + 1);
         }
      }
      #endregion
   }
}
