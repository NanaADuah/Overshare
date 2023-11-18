using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

namespace Overshare.Data
{
    [Authorize]
    public class LogoutUser
    {
        public static void Logout(User user)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated ) 
            {
                if (user != null)
                {
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("~/login.aspx");
                }
            }
        }
    }
}