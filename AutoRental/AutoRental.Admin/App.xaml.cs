using AutoRental.Client.Bootstrapper;
using NP.Core.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Windows;

namespace AutoRental.Admin
{
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
         {
            new AssemblyCatalog(Assembly.GetExecutingAssembly())
         });
      }
   }
}
