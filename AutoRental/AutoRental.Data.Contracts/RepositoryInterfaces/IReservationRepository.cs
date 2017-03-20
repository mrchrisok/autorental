using AutoRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRental.Data.Contracts
{
    public interface IReservationRepository : IDataRepository<Reservation>
    {
        IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate);
        IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo();
        IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId);
    }
}
