using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Runtime.Serialization;
using NPCCC = NP.Core.Common.Core;

namespace AutoRental.Business.Entities
{
   [DataContract]
    public class Sale : NPCCC.EntityBase, IIdentifiableEntity, IAccountOwnedEntity
   {
      #region Accessors

      [DataMember]
      public int SaleId { get; set; }

      [DataMember]
      public int AccountId { get; set; }

      [DataMember]
      public int CarId { get; set; }

      [DataMember]
      public DateTime SaleDate { get; set; }

      [DataMember]
      public decimal SaleAmount { get; set; }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Members.IIdentifiableEntity

      int IIdentifiableEntity.EntityID
      {
         get { return SaleId; }
         set { SaleId = value; }
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
