using AttributeRouting.Web.Http;
using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using AutoRental.Web.Mvc.Core;
using AutoRental.Web.Mvc.Models;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoRental.Web.Mvc.Controllers.Api
{
   [Export]
   [PartCreationPolicy(CreationPolicy.NonShared)]
   [Authorize]
   [UsesDisposableServiceAttribute]
   public class RentalApiController : ApiControllerBase
   {
      #region Properties

      IRentalService _RentalService;
      IInventoryService _InventoryService;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Constructors

      [ImportingConstructor]
      public RentalApiController(IRentalService rentalService, IInventoryService inventoryService)
      {
         _RentalService = rentalService;
         _InventoryService = inventoryService;
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods.Override

      protected override void RegisterServices(List<IServiceContract> disposableServices)
      {
         disposableServices.Add(_RentalService);
         disposableServices.Add(_InventoryService);
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods

      //--// GET
      [HttpGet]
      [GET("api/rental/getrentalhistory")]
      public HttpResponseMessage GetRentalHistory(HttpRequestMessage request)
      {
         return GetHttpResponse(request, () =>
         {
            string user = User.Identity.Name; // this method is secure to only the authenticated user to reserve
            CustomerRentalData[] rentals = _RentalService.GetCustomerRentals(user);
            HttpResponseMessage response = request.CreateResponse<CustomerRentalData[]>(HttpStatusCode.OK, rentals);

            return response;
         });
      }
      #endregion

   }
}
