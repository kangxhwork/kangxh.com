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

//https://docs.microsoft.com/en-us/azure/key-vault/key-vault-use-from-web-application

namespace kangxh.com.Identity
{
    public partial class AppClientID : System.Web.UI.Page
    {
        public static string EncryptSecret { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            var secret = Task.Run(async () => await kvClient.GetSecretAsync(WebConfigurationManager.AppSettings["SecretUri"])).Result;
            Response.Write(secret.Value);

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