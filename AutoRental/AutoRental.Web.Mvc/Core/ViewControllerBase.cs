using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Core
{
    public class ViewControllerBase : Controller
    {
        //
        #region Accessors

        List<IServiceContract> _DisposableServices;
        List<IServiceContract> DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();

                return _DisposableServices;
            }
        }
        #endregion


        //
        #region Methods.Virtual

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {
        }
        #endregion


        //
        #region Methods.Overrides

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            RegisterServices(DisposableServices);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            foreach (var service in DisposableServices)
            {
                if (service != null && service is IDisposable)
                    (service as IDisposable).Dispose();
            }
        }
        #endregion
    }
}