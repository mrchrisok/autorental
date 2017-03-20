using AutoRental.Common;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Web.Http;

namespace AutoRental.Web.Mvc.Core
{
   public class ApiControllerBase : ApiController, IServiceAwareController
   {
      #region Properties

      List<IServiceContract> _DisposableServices;
      #endregion

      //-----------------------------------------------------------------------------------------------
      #region Actions

      protected virtual void RegisterServices(List<IServiceContract> disposableServices)
      {
         // virtual method .. can be tailored by inheritors
      }

      protected void ValidateAuthorizedUser(string userRequested)
      {
         string userLoggedIn = User.Identity.Name;
         if (userLoggedIn != userRequested)
            throw new SecurityException("Attempting to access data for another user.");
      }

      protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
      {
         HttpResponseMessage response = null;

         try
         {
            response = codeToExecute.Invoke();
         }
         catch (SecurityException ex)
         {
            response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
         }
         catch (FaultException<AuthorizationValidationException> ex)
         {
            response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
         }
         catch (FaultException ex)
         {
            response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
         }
         catch (Exception ex)
         {
            response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
         }

         return response;
      }
      #endregion

      //-----------------------------------------------------------------------------------------------
      #region Members.IServiceAwareController

      List<IServiceContract> IServiceAwareController.DisposableServices
      {
         get
         {
            if (_DisposableServices == null)
               _DisposableServices = new List<IServiceContract>();

            return _DisposableServices;
         }
      }

      void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> disposableServices)
      {
         RegisterServices(disposableServices); 
      }
      #endregion
   }
}