using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Overshare.Data
{
    public class LogoutUser
    {
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}