using NP.Core.Common.ServiceModel;
using System;
using System.Runtime.Serialization;

namespace AutoRental.Client.Contracts
{
   [DataContract]
   public class CustomerReservationData : DataContractBase
   {
      [DataMember]
      public int ReservationId { get; set; }

      [DataMember]
      public string CustomerName { get; set; }

      [DataMember]
      public string Car { get; set; }

      [DataMember]
      public DateTime RentalDate { get; set; }

      [DataMember]
      public DateTime ReturnDate { get; set; }
   }
}
