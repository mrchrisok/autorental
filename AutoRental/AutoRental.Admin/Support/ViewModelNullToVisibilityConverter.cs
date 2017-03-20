using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutoRental.Admin.Support
{
   public class ViewModelNullToVisibilityConverter : IValueConverter
   {
      object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return (value == null ? Visibility.Collapsed : Visibility.Visible);
      }

      object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
