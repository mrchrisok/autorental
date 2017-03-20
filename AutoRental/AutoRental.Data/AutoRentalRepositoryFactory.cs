using Core.Common.Contracts;
using NP.Core.Common.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Data
{
   [Export(typeof(IDataRepositoryFactory))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   public class AutoRentalRepositoryFactory : IDataRepositoryFactory
   {
      T IDataRepositoryFactory.GetDataRepository<T>()
      {
         return ObjectBase.Container.GetExportedValue<T>();
      }
   }
}

