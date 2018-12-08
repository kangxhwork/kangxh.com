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
            }
        }
    }
}