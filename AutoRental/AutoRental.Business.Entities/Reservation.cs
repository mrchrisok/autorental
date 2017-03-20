using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Runtime.Serialization;
using NPCCC = NP.Core.Common.Core;

namespace AutoRental.Business.Entities
{
   [DataContract]
    public class Reservation : NPCCC.EntityBase, IIdentifiableEntity, IAccountOwnedEntity
   {
      #region Accessors

      [DataMember]
      public int ReservationId { get; set; }

      [DataMember]
      public int AccountId { get; set; }

      [DataMember]
      public int CarId { get; set; }

      [DataMember]
      public DateTime RentalDate { get; set; }

      [DataMember]
      public DateTime ReturnDate { get; set; }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Members.IIdentifiableEntity

      int IIdentifiableEntity.EntityID
      {
         get { return ReservationId; }
         set { ReservationId = value; }
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
