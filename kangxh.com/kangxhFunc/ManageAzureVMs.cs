using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace kangxhFunc
{
    public static class ManageAzureVMs
    {

        public static string tenantID = ConfigurationManager.AppSettings["tenantID"];
        public static string subscriptionID = ConfigurationManager.AppSettings["subscriptionID"];
        public static string spnID = ConfigurationManager.AppSettings["spnID"];
        public static string spnPassword = ConfigurationManager.AppSettings["spnPassword"];

        [FunctionName("ManageAzureVMs")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            
            string URL = "https://management.azure.com/subscriptions/" + "{subscriptionid}" +
                "/resourceGroups/" + "{resourcegroupname}" + "/providers/Microsoft.Compute/virtualMachines/" +
                "{vmname}/" + "{controlcode}" +
                "?api-version=2018-06-01";

            dynamic body = await req.Content.ReadAsStringAsync();
            //dynamic body = Task.Run(async () => { return await req.Content.ReadAsStringAsync(); }).Result;
            var json = JsonConvert.DeserializeObject<VMControl>(body as string);

            URL = URL.Replace("{subscriptionid}", subscriptionID)
                     .Replace("{resourcegroupname}", json.RGName)
                     .Replace("{vmname}", json.VMName)
                     .Replace("{controlcode}", json.ControlCode);

            string token = await GetAccessTokenAsync(tenantID, spnID, spnPassword);

            // the PostAzureAPIAsync can suspend for few minutes, return the http request before the task complete
            Task.Run(() => PostAzureAPIAsync(URL, token, json.Suspend));
            return req.CreateResponse(HttpStatusCode.OK, URL);
            
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

        private static async Task<string> GetAccessTokenAsync(string tenantId, string clientId, string clientKey)
        {
            string authContextURL = "https://login.windows.net/" + tenantId;
            var authenticationContext = new AuthenticationContext(authContextURL);
            var credential = new ClientCredential(clientId, clientKey);
            var result = await authenticationContext
                .AcquireTokenAsync("https://management.azure.com/", credential);

            if (result == null)
            {
                throw new System.InvalidOperationException("Failed to obtain the JWT token");
            }
            string token = result.AccessToken;
            return token;
        }

        private static async Task<string> PostAzureAPIAsync(string URL, string token, int suspend)
        {
            Thread.Sleep(suspend * 1000 * 60);

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await httpClient.PostAsync(URL, null);

            return response.StatusCode.ToString();
        }
    }
}
