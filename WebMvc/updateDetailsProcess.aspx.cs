using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMvc
{
    public partial class updateDetailsProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //קבלת הנתונים מהבקשה
            string username = Request.Form["username"].Trim();
            string password = Request.Form["password"].Trim();
            string email = Request.Form["email"].Trim();
            //שדות רשות - אם השדה נול נכניס מחרוזת ריקה
            string country = Request.Form["country"] == null ? "" : Request.Form["country"].Trim();
            int age = Request.Form["age"] == null ? 0 : int.Parse(Request.Form["age"].Trim());

            //אריזה בעצם מטיפוס משתמש
            UserSystem.User newDetails = new UserSystem.User(
                username,
                password,
                email,
                country,
                age,
                false
                );
            //השם המקורי
            string originalName = UserSystem.LoggedInUser.GetUsername();
            //עדכון בבסיס הנתונים
            bool updateSucceed = UserSystem.UserController.UpdateUserDetails(originalName, newDetails);

            //בדיקה אם עדכון הצליח
            if (updateSucceed)
                Response.Write("Update done successfully");
            else
                Response.Write("The username is taken!");

        }
    }
}