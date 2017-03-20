using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SandboxUI.Sandbox
{
   public partial class SandboxProcessing : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (IsPostBack)
         {
            string fullName = Request.Form["fullname"];
            string description = Request.Form["description"];
            string employmentStatus = Request.Form["employment-status"];
            bool indentationSkills = !string.IsNullOrEmpty(Request.Form["indentation"]);
            bool fastTyper = !string.IsNullOrEmpty(Request.Form["fast"]);
            bool resumeInGit = !string.IsNullOrEmpty(Request.Form["git"]);
            string bonus = Request.Form["bonus"];

            Response.StatusCode = 200; // hard code a fail
         }
      }
   }
}