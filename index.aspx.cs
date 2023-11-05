using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Overshare
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["User"] is Data.User)
            {
                // A user is logged in. Redirect to the home page.
                Response.Redirect("/drive/drive.aspx");
            }
            else
            {
                // No user is logged in. Redirect to the login page.
                Response.Redirect("Login.aspx");
            }
        }
    }
}