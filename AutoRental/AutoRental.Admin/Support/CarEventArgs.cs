using AutoRental.Client.Entities;
using System;

namespace AutoRental.Admin.Support
{
   public class CarEventArgs : EventArgs
   {
      public CarEventArgs(Car car, bool isNew)
      {
         Car = car;
         IsNew = isNew;
      }

      public Car Car { get; set; }
      public bool IsNew { get; set; }
   }
}
