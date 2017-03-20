using NP.Core.Common.ServiceModel;
using System;
using System.Runtime.Serialization;

namespace AutoRental.Client.Contracts
{
   [DataContract]
   public class CustomerRentalData : DataContractBase
   {
      [DataMember]
      public int RentalId { get; set; }

      [DataMember]
      public string CustomerName { get; set; }

      [DataMember]
      public string Car { get; set; }

      [DataMember]
      public DateTime DateRented { get; set; }

      [DataMember]
      public DateTime ExpectedReturn { get; set; }

      [DataMember]
      public DateTime? DateReturned { get; set; }
   }
}

