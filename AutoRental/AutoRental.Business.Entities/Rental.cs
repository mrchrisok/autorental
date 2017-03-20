using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Runtime.Serialization;
using NPCCC = NP.Core.Common.Core;

namespace AutoRental.Business.Entities
{
   [DataContract]
    public class Rental : NPCCC.EntityBase, IIdentifiableEntity, IAccountOwnedEntity
   {
      #region Accessors

      [DataMember]
      public int RentalId { get; set; }

      [DataMember]
      public int AccountId { get; set; }

      [DataMember]
      public int CarId { get; set; }

      [DataMember]
      public DateTime DateRented { get; set; }

      [DataMember]
      public DateTime DateDue { get; set; }

      [DataMember]
      public DateTime? DateReturned { get; set; }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Members.IIdentifiableEntity

      int IIdentifiableEntity.EntityID
      {
         get { return RentalId; }
         set { RentalId = value; }
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Members.IAccountOwnedEntity

      int IAccountOwnedEntity.OwnerAccountID
      {
         get { return AccountId; }
      }
      #endregion
   }
}
