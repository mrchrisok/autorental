using AttributeRouting.Web.Mvc;
using AutoRental.Web.Mvc.Core;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Controllers.Mvc
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class CustomerController : ViewControllerBase
    {
        //------------------------------------------------------------------------------------
        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        //--// GET
        [HttpGet]
        [GET("customer/account")]
        public ActionResult MyAccount()
        {
            return View();
        }

        [HttpGet]
        [GET("customer/reserve")]
        public ActionResult ReserveCar()
        {
            return View();
        }

        [HttpGet]
        [GET("customer/reservations")]
        public ActionResult CurrentReservations()
        {
            return View();
        }

        [HttpGet]
        [GET("customer/history")]
        public ActionResult RentalHistory()
        {
            return View();
        }
        #endregion
    }
}
