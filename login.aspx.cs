using Overshare.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Overshare
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            string email = emailInput.Value;
            string password = passwordInput.Value;

            User user = UserMethods.Authenticate(email, password);

            if (user != null)
            {
                // Store the authenticated user in a session
                Session["User"] = user;

                // Redirect to the dashboard upon successful login
                FormsAuthentication.SetAuthCookie(email, chkLoggedIn.Checked);
                UserAccount.EnsureUserFolderAndFilesExist(user.UserID);
                Response.Redirect("/drive/drive.aspx");

            }
            else
            {
                lblError.Text = "Login failed. Please check your credentials.";
            }
        }
    }
}