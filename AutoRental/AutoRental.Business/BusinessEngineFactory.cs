using NP.Core.Business.Contracts;
using NP.Core.Common.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Business
{
   [Export(typeof(IBusinessEngineFactory))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class BusinessEngineFactory : IBusinessEngineFactory
   {
      T IBusinessEngineFactory.GetBusinessEngine<T>()
      {
         return ObjectBase.Container.GetExportedValue<T>();
      }
   }
}
