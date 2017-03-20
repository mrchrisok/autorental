using AttributeRouting.Web.Mvc;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace AutoRental.Web.Mvc.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        //--------------------------------------------------------------------------------------

        #region Methods
        public ActionResult Index()
        {
            return View();
        }

        //--// GET
        [HttpGet]
        [GET("home/my")]  //oco: attribute routing enabled by adding attribute routing nuget pkg
        [Authorize]
        public ActionResult MyAccount()
        {
            return View();
        }
        #endregion
    }
}

