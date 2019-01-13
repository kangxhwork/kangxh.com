using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;

namespace kangxh.com.container
{
    public partial class managek8scluster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string webhookURL = (WebConfigurationManager.ConnectionStrings["StartVMWebhookURL"]).ConnectionString;
            string ManageAzureVMsFuncURL = (WebConfigurationManager.ConnectionStrings["ManageAzureVMsFunc"]).ConnectionString;

            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Response.Write("Manage the K8S VMs <br>");

                //post data to webhooker
                Response.Write("Starting K8S VMs but send request to runbook web hooker <br>");
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                webClient.UploadString(webhookURL, "");

                // post data to function url to stop three vms. 
                Uri functionUri = new Uri(ManageAzureVMsFuncURL);
                for (int i = 1; i <= 3; i++)
                {
                    VMControl vmControl = new VMControl("kangxhvmseaoss"+i.ToString(), "az-rg-kangxh-oss", "deallocate", 5);
                    string manageAzureVMsFuncContent = JsonConvert.SerializeObject(vmControl);
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.UploadString(ManageAzureVMsFuncURL, manageAzureVMsFuncContent);

                    Response.Write("queue tasks to Functions so that " + vmControl.VMName + " will be turn off after 5min <br>");
                }
            }

            Response.Redirect("http://www.kangxh.com:83");
        }

        public class VMControl
        {
            public string VMName { get; set; }
            public string RGName { get; set; }
            public string ControlCode { get; set; } // Start, Stop as the control code.
            public int Suspend { get; set; } // suspend time (in minute) to execute.
            public VMControl(string _vmName, string _rgName, string _controlCode, int _suspend)
            {
                VMName = _vmName;
                RGName = _rgName;
                ControlCode = _controlCode;
                Suspend = _suspend;
            }
        }
    }
}