using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Security.Claims;
using System.Collections;

namespace kangxh.com.Identity
{
    public partial class claims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (claimsPrincipal != null)
            {
                foreach (Claim claim in claimsPrincipal.Claims)
                {
                    Response.Write("CLAIM TYPE: " + claim.Type + "</br>CLAIM VALUE: " + claim.Value + "</br></br>");
                }
            }
            else
            {

            }
        }
    }
}