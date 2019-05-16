using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.WindowsAzure;
using System.Threading.Tasks;
using System.Web.Configuration;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace kangxh.com.IaaS
{
    /// <summary>
    /// Summary description for imghandler
    /// </summary>
    public class imghandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Trace.TraceInformation("get request from " + context.Request.Browser);
            context.Response.ContentType = "text/plain";

            string storageConnectStr = WebConfigurationManager.ConnectionStrings["StorageAccount"].ConnectionString;
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("image");
            cloudBlobContainer.CreateIfNotExists();

            string fileGUID = Guid.NewGuid().ToString();
            // upload file to container
            try
            {
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];

                    // generate new name
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName) + fileGUID.Substring(0, 8) + Path.GetExtension(file.FileName);

                    CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    blob.UploadFromStream(file.InputStream);
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
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