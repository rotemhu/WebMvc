using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMvc
{
    public partial class loginProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.Form["usr"];
            string password = Request.Form["pass"];

            if (UserSystem.UserController.Login(username, password))
                Response.Redirect(Request.UrlReferrer.ToString());
            else
                Response.Redirect("login.aspx?success=false");
        }
    }
}