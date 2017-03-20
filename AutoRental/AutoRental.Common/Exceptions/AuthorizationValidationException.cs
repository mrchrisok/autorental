using System;

namespace AutoRental.Common
{
   public class AuthorizationValidationException : Exception
   {
      #region Constructors

      public AuthorizationValidationException(string message)
         : this(message, null)
      {
      }

      public AuthorizationValidationException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
      #endregion
   }
}
