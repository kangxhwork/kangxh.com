using System.Linq;
using System;
using System.Configuration;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading.Tasks;
using System.Web.Configuration;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

//https://docs.microsoft.com/zh-cn/azure/storage/common/storage-dotnet-shared-access-signature-part-1

namespace kangxh.com.html5.IaaS
{
    public partial class Blob : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            string clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            clientIP = clientIP.Substring(0, clientIP.IndexOf(':'));
            IPAddressOrRange clientIPRange = new IPAddressOrRange(clientIP);

            ConnectionStringSettings storageConnectionStr = WebConfigurationManager.ConnectionStrings["StorageAccount"];

            string ConnectionString = storageConnectionStr.ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            //Create container webcam if it does not exist.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("webcam");
            blobContainer.CreateIfNotExists();

            // Create a new access policy for the account.
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.List,
                Services = SharedAccessAccountServices.Blob | SharedAccessAccountServices.File,
                ResourceTypes = SharedAccessAccountResourceTypes.Service,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1),
                Protocols = SharedAccessProtocol.HttpsOnly,
                IPAddressOrRange = clientIPRange
            };

            // Return the SAS token and container Uri. Javascript will use these value to upload image. 
            string sasToken = storageAccount.GetSharedAccessSignature(policy);
            blobContainerEntry.Text = blobClient.StorageUri.PrimaryUri + "/webcam";
            sasTokenTextBox.Text = sasToken;
        }
    }
}