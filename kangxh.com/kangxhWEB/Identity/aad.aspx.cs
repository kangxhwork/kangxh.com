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

using System.Security.Claims;
using System.Security.Principal;


namespace kangxh.com.Identity
{
    public partial class aad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SignInAAD( sender, e);
        }
        protected void SignInAAD(object sender, EventArgs e)
        {
            // Send a WSFederation sign-in request.
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/identity/claims.aspx" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}
