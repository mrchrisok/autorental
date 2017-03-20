using AutoRental.Data.Repositories;
using System.ComponentModel.Composition.Hosting;

namespace AutoRental.Business.Bootstrapper
{
   public static class MEFLoader
   {
      public static CompositionContainer Init()
      {
         AggregateCatalog catalog = new AggregateCatalog();

         // scan and register DI/[Export] objects from the assembly of the given type
         catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly)); //AutoRental.Data
         catalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoRentalEngine).Assembly));  //AutoRental.Business

         CompositionContainer container = new CompositionContainer(catalog);
         return container;
      }
   }
}
