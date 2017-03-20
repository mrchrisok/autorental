using AttributeRouting.Web.Http;
using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using AutoRental.Web.Mvc.Core;
using AutoRental.Web.Mvc.Models;
using Core.Common.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoRental.Web.Mvc.Controllers.Api
{
   [Export]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   [Authorize]
   [UsesDisposableService]
   public class CustomerApiController : ApiControllerBase
   {
      #region Properties

      IAccountService _AccountService;
      #endregion

      //---------------------------------------------------------------------------------------------------
      #region Constructors

      [ImportingConstructor]
      public CustomerApiController(IAccountService accountService)
      {
         _AccountService = accountService;
      }
      #endregion

      //---------------------------------------------------------------------------------------------------
      #region Methods.Override

      protected override void RegisterServices(List<IServiceContract> disposableServices)
      {
         disposableServices.Add(_AccountService);
      }
      #endregion

      //---------------------------------------------------------------------------------------------------
      #region Methods

      [HttpGet]
      [GET("api/customer/account")]
      public HttpResponseMessage GetCustomerAccountInfo(HttpRequestMessage request)
      {
         return GetHttpResponse(request, () =>
         {
            HttpResponseMessage response = null;

            Account account = _AccountService.GetCustomerAccountInfo(User.Identity.Name);
            // notice no need to create a seperate model object since Account entity will do just fine

            response = request.CreateResponse<Account>(HttpStatusCode.OK, account);

            return response;
         });
      }

      [HttpPost]
      [POST("api/customer/account")]
      public HttpResponseMessage UpdateCustomerAccountInfo(HttpRequestMessage request, Account accountModel)
      {
         return GetHttpResponse(request, () =>
         {
            HttpResponseMessage response = null;

            ValidateAuthorizedUser(accountModel.LoginEmail);

            List<string> errors = new List<string>();

            List<State> states = UIHelper.GetStates();
            State state = states.Where(item => item.Abbrev.ToUpper() == accountModel.State.ToUpper()).FirstOrDefault();
            if (state == null)
               errors.Add("Invalid state.");

            // trim out the / in the exp date
            accountModel.ExpDate = accountModel.ExpDate.Substring(0, 2) + accountModel.ExpDate.Substring(3, 2);

            if (errors.Count == 0)
            {
               _AccountService.UpdateCustomerAccountInfo(accountModel);
               response = request.CreateResponse(HttpStatusCode.OK);
            }
            else
               response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());

            return response;
         });
      }
      #endregion
   }
}
