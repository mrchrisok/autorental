using AutoRental.Client.Proxies;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace AutoRental.Client.Bootstrapper
{
   public static class MEFLoader
   {
      //--------------------------------------------------------------------------------------
      #region Methods

      public static CompositionContainer Init()
      {
         return Init(null);
      }

      public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
      {
         AggregateCatalog catalog = new AggregateCatalog();
         catalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryClient).Assembly));

         if (catalogParts != null)  // discover objects in passed in assembly catalog
            foreach (var part in catalogParts)
               catalog.Catalogs.Add(part);

         CompositionContainer container = new CompositionContainer(catalog);

         return container;
      }
      #endregion
   }
}
