using System;

namespace AutoRental.Common
{
   public class CarCurrentlyRentedException : Exception
   {
      public CarCurrentlyRentedException(string message)
         : base(message)
      {
      }

      public CarCurrentlyRentedException(string message, Exception ex)
         : base(message, ex)
      {
      }
   }
}