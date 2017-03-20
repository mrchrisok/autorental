using NP.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Core
{
    public class MefDependencyResolver : IDependencyResolver
    {
        //
        #region Accessors

        CompositionContainer _Container;
        #endregion

        //
        #region Constructors

        public MefDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }
        #endregion

        //
        #region Methods

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }
        #endregion
    }
}