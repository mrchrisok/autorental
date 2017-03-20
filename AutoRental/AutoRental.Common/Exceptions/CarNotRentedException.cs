using System;

namespace AutoRental.Common
{
   public class CarNotRentedException : Exception
   {
      public CarNotRentedException(string message)
         : base(message)
      {
      }

      public CarNotRentedException(string message, Exception ex)
         : base(message, ex)
      {
      }
   }
}
