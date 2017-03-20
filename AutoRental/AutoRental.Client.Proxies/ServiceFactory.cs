using Core.Common.Contracts;
using NP.Core.Common.Core;
using System.ComponentModel.Composition;

namespace AutoRental.Client.Proxies
{
    [Export(typeof(IServiceFactory))]  // MefDI: interface mapping
    [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
    public class ServiceFactory : IServiceFactory
    {
        //----------------------------------------------------------------------------
        #region Operations.Interface.IServiceFactory

        T IServiceFactory.CreateClient<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
        #endregion
    }
}
