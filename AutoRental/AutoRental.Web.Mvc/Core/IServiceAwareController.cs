using Core.Common.Contracts;
using System.Collections.Generic;

namespace AutoRental.Web.Mvc.Core
{
    public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);
        List<IServiceContract> DisposableServices { get; }
    }
}
