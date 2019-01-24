using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using System.Threading.Tasks;
using System.Web.Configuration;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
namespace Sites
{
    /// <summary>
    /// Summary description for photo
    /// </summary>
    public class photo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Trace.TraceInformation("start storage");
            context.Response.ContentType = "text/plain";

            string storageConnectStr = WebConfigurationManager.ConnectionStrings["StorageAccount"].ConnectionString;
            
            string photoContainer = context.Request["trip"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectStr);

            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(photoContainer);
            cloudBlobContainer.CreateIfNotExists();

            string fileGUID = Guid.NewGuid().ToString();
            // upload file to container
            try
            {
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];

                    string fileName = fileGUID.Substring(0,8) + "-" + file.FileName;
                    string fileExtension = file.ContentType;

                    CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    blob.UploadFromStream(file.InputStream);
                }
            }
            catch (Exception ac)
            {
                context.Response.Write("error");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}