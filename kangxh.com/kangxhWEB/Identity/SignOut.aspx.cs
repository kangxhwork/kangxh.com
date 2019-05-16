using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Microsoft.Owin.Security.OpenIdConnect;

namespace kangxh.com.Identity
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Signout();
        }

        protected void Signout()
        {

            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = "http://www.kangxh.com/" },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}