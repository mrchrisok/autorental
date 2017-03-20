using AutoRental.Business.Entities;

namespace AutoRental.Data.Contracts
{
    public class CustomerReservationInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Reservation Reservation { get; set; }
    }
}
