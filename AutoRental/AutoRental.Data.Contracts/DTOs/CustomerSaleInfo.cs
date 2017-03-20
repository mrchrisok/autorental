using AutoRental.Business.Entities;

namespace AutoRental.Data.Contracts
{
   public class CustomerSaleInfo
   {
      public Account Customer { get; set; }
      public Car Car { get; set; }
      public Sale Sale { get; set; }
   }
}
