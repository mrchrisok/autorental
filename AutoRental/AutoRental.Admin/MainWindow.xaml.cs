using AutoRental.Admin.ViewModels;
using NP.Core.Common.Core;
using MahApps.Metro.Controls;

namespace AutoRental.Admin
{
   public partial class MainWindow : MetroWindow
   {
      public MainWindow()
      {
         InitializeComponent();

         main.DataContext = ObjectBase.Container.GetExportedValue<MainViewModel>();
      }
   }
}
