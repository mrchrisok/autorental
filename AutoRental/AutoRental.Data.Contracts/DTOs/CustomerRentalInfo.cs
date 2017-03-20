using AutoRental.Business.Entities;

namespace AutoRental.Data.Contracts
{
    public class CustomerRentalInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Rental Rental { get; set; }
    }
}
