using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Sites._2019US
{
    /// <summary>
    /// Summary description for fileUploader
    /// </summary>
    public class fileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];

                    context.Response.Write(file.FileName);
                }

                context.Response.Write(context.Request.Form.AllKeys);

                context.Response.Write(str_image);
            }
            catch (Exception ac)
            {

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