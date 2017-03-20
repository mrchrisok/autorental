using System;
using System.Runtime.Serialization;

namespace AutoRental.Common
{
   public class UnableToRentForDateException : Exception
   {
      public UnableToRentForDateException(string message)
         : base(message)
      {
      }

      public UnableToRentForDateException(string message, Exception ex)
         : base(message, ex)
      {
      }
   }
}
