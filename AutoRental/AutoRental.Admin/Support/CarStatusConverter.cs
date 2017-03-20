using System;
using System.Globalization;
using System.Windows.Data;

namespace AutoRental.Admin.Support
{
   public class CarStatusConverter : IValueConverter
   {
      object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         bool currentlyRented = (bool)value;

         return (currentlyRented ? "Currently Rented" : "Available");
      }

      object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}

