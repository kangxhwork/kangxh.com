using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace kangxh.com.Identity
{
    public partial class AppClientID : System.Web.UI.Page
    {
        public static string EncryptSecret { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            
            Response.Write(WebConfigurationManager.AppSettings["ClientID"]);
            Response.Write("</br>");
            Response.Write(WebConfigurationManager.AppSettings["ClientSecret"]);
            Response.Write("</br>");
            Response.Write(WebConfigurationManager.AppSettings["SecretUri"]);
            Response.Write("</br>");

            Task.WaitAll(ReadSecret());
            Response.Write (EncryptSecret);
        }

        protected static async Task<string> ReadSecret()
        {
            var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            var kvSecret = await kvClient.GetSecretAsync(WebConfigurationManager.AppSettings["SecretUri"]);

            EncryptSecret = kvSecret.Value;

            return EncryptSecret;
        }

        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"],
                        WebConfigurationManager.AppSettings["ClientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

    }
}