using AttributeRouting.Web.Mvc;
using AutoRental.Common.Contracts;
using AutoRental.Web.Mvc.Core;
using AutoRental.Web.Mvc.Models;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace AutoRental.Web.Mvc.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountController : ViewControllerBase
    {
        #region Properties

        ISecurityAdapter _SecurityAdapter;
        #endregion

        //------------------------------------------------------------------------------------
        #region Constructors

        [ImportingConstructor] // used by Mef
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _SecurityAdapter = securityAdapter;
        }
        #endregion

        //------------------------------------------------------------------------------------
        #region Methods

        //--// GET
        [HttpGet]
        [GET("account/register")]
        public ActionResult Register()
        {
            _SecurityAdapter.Initialize();
            return View();
        }

        [HttpGet]
        [GET("account/login")]
        public ActionResult Login(string returnUrl)
        {
            _SecurityAdapter.Initialize();
            return View(new AccountLoginModel() { ReturnUrl = returnUrl });
        }

        [HttpGet]
        [GET("account/logout")]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [GET("account/changepassword")]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [GET("account/forgotpassword")]
        [Authorize]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion
    }
}
