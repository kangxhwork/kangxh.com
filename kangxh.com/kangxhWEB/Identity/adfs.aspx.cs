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

namespace kangxh.com.Identity
{

    public partial class adfs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SignInADFS();
        }
        protected void SignInADFS()
        {
            // Send a WSFederation sign-in request.
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/identity/claims.aspx" },
                    WsFederationAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}