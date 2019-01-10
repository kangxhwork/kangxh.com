using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace kangxh.com.container
{
    public partial class managek8scluster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void startK8sCluster_Click(object sender, EventArgs e)
        {
            string webhookURL = (WebConfigurationManager.ConnectionStrings["StartVMWebhookURL"]).ConnectionString;
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string postResult = webClient.UploadString(webhookURL, "");
                Response.Write(postResult);
            }
        }
    }
}