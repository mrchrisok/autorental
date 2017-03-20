using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRental.Admin.Support
{
   public class RentalDeniedEventArgs : EventArgs
   {
      public string ErrorMessage { get; set; }

      public RentalDeniedEventArgs(string errorMessage)
      {
         ErrorMessage = errorMessage;
      }
   }
}
