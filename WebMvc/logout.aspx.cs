using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMvc
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ניתוק המשתמש מהמערכת
            UserSystem.UserController.Logout();

            //חזרה לדף שקרא לניתוק
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}