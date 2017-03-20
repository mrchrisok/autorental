using Core.Common.Contracts;
using Core.Common.Core;
using System.Runtime.Serialization;
using System;
using NPCCC = NP.Core.Common.Core;

namespace AutoRental.Business.Entities
{
   [DataContract]
    public class Car : NPCCC.EntityBase, IIdentifiableEntity
   {
      //--------------------------------------------------------------------------------
      #region Constructors

      public Car() { }

      public Car(string description, string color, int year, decimal rentalPrice, DateTime dateCreated)
      {
         Description = description;
         Color = color;
         Year = year;
         RentalPrice = rentalPrice;
         DateCreated = dateCreated;
      }
      #endregion

      //-----------------------------------------------------------------------------------------------------
      #region Accessors

      [DataMember]
      public int CarId { get; set; }

      [DataMember]
      public string Description { get; set; }

      [DataMember]
      public string VIN { get; set; }

      [DataMember]
      public string Color { get; set; }

      [DataMember]
      public int Year { get; set; }

      [DataMember]
      public decimal RentalPrice { get; set; }

      [DataMember]
      public bool CurrentlyRented { get; set; }

      [DataMember]
      public decimal PurchasePrice { get; set; }  

      [DataMember]
      public decimal PurchaseDate { get; set; }
      #endregion

      //-----------------------------------------------------------------------------------------------------
      #region Members.IIdentifiableEntity

      int IIdentifiableEntity.EntityID
      {
         get { return CarId; }
         set { CarId = value; }
      }
      #endregion
   }
}
