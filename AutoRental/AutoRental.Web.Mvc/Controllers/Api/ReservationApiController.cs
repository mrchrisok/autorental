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
   public class ReservationApiController : ApiControllerBase
   {
      #region Properties

      IInventoryService _InventoryService;
      IRentalService _RentalService;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Constructors

      [ImportingConstructor]
      public ReservationApiController(IInventoryService inventoryService, IRentalService rentalService)
      {
         _InventoryService = inventoryService;
         _RentalService = rentalService;
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods.Override

      protected override void RegisterServices(List<IServiceContract> disposableServices)
      {
         disposableServices.Add(_InventoryService);
         disposableServices.Add(_RentalService);
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods

      //--// GET
      [HttpGet]
      [AllowAnonymous]
      [GET("api/reservation/availablecars")]
      public HttpResponseMessage GetAvailableCars(HttpRequestMessage request, DateTime pickupDate, DateTime returnDate)
      {
         return GetHttpResponse(request, () =>
         {
            Car[] cars = _InventoryService.GetAvailableCars(pickupDate, returnDate);

            return request.CreateResponse<Car[]>(HttpStatusCode.OK, cars);
         });
      }

      [HttpGet]
      [GET("api/reservation/getopen")]
      public HttpResponseMessage GetOpenReservations(HttpRequestMessage request)
      {
         return GetHttpResponse(request, () =>
         {
            HttpResponseMessage response = null;

            string user = User.Identity.Name; // this method is secure to only the authenticated user to reserve
            CustomerReservationData[] reservations = _RentalService.GetCustomerReservations(user);

            response = request.CreateResponse<CustomerReservationData[]>(HttpStatusCode.OK, reservations);

            return response;
         });
      }

      //--// POST
      [HttpPost]
      [POST("api/reservation/reservecar")]
      public HttpResponseMessage ReserveCar(HttpRequestMessage request, [FromBody]ReservationModel reservationModel)
      {
         return GetHttpResponse(request, () =>
         {
            HttpResponseMessage response = null;

            string user = User.Identity.Name; // this method is secure to only the authenticated user to reserve
            Reservation reservation = _RentalService.MakeReservation(user, reservationModel.Car, reservationModel.PickupDate, reservationModel.ReturnDate);

            response = request.CreateResponse<Reservation>(HttpStatusCode.OK, reservation);

            return response;
         });
      }

      [HttpPost]
      [POST("api/reservation/cancel")]
      public HttpResponseMessage CancelReservation(HttpRequestMessage request, [FromBody]int reservationId)
      {
         return GetHttpResponse(request, () =>
         {
            HttpResponseMessage response = null;

            // not that calling the WCF service here will authenticate access to the data
            Reservation reservation = _RentalService.GetReservation(reservationId);
            if (reservation != null)
            {
               _RentalService.CancelReservation(reservationId);

               response = request.CreateResponse(HttpStatusCode.OK);
            }
            else
               response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No reservation found under that ID.");

            return response;
         });
      }
      #endregion

   }
}
