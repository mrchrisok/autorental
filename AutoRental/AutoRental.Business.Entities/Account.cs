using Core.Common.Contracts;
using Core.Common.Core;
using System.Runtime.Serialization;
using NPCCC = NP.Core.Common.Core;

namespace AutoRental.Business.Entities
{
   [DataContract]
   public class Account : NPCCC.EntityBase, IIdentifiableEntity, IAccountOwnedEntity
   {
      //--------------------------------------------------------------------------------
      #region Accessors

      [DataMember]
      public int AccountId { get; set; }

      [DataMember]
      public string LoginEmail { get; set; }

      [DataMember]
      public string FirstName { get; set; }

      [DataMember]
      public string LastName { get; set; }

      [DataMember]
      public string Address { get; set; }

      [DataMember]
      public string City { get; set; }

      [DataMember]
      public string State { get; set; }

      [DataMember]
      public string ZipCode { get; set; }

      [DataMember]
      public string CreditCard { get; set; }

      [DataMember]
      public string ExpDate { get; set; }
      #endregion

      //--------------------------------------------------------------------------------
      #region Members.IIdentifiableEntity

      int IIdentifiableEntity.EntityID
      {
         get { return AccountId; }
         set { AccountId = value; }
      }
      #endregion

      //--------------------------------------------------------------------------------
      #region Members.IAccountOwnedEntity

      int IAccountOwnedEntity.OwnerAccountID
      {
         get { return AccountId; }
      }
      #endregion
   }
}
