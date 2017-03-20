using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AutoRental.Web.Mvc.Core
{
   public class UsesDisposableServiceAttribute : ActionFilterAttribute
   {
      //------------------------------------------------------------------------------------------------
      #region Methods.Override

      public override void OnActionExecuting(HttpActionContext actionContext)
      {
         // pre-processing

         IServiceAwareController controller = actionContext.ControllerContext.Controller as IServiceAwareController;
         if (controller != null)
         {
            controller.RegisterDisposableServices(((IServiceAwareController)controller).DisposableServices);
         }
      }

      public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
      {
         //post-processing

         IServiceAwareController controller = actionExecutedContext.ActionContext.ControllerContext.Controller as IServiceAwareController;
         if (controller != null)
         {
            foreach (var service in controller.DisposableServices)
            {
               if (service != null && service is IDisposable)
                  (service as IDisposable).Dispose();
            }
         }
      }
      #endregion
   }
}