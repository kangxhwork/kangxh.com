using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

using ImageResizer;

// https://docs.microsoft.com/en-us/azure/functions/tutorial-static-website-serverless-api-with-database
// https://blogs.msdn.microsoft.com/martinkearn/2016/05/06/smart-image-re-sizing-with-azure-functions-and-cognitive-services/
namespace kangxhFunc
{
    public static class CreateThumbnail
    {
        public static string _apiKey = ConfigurationManager.AppSettings["CognitiveAPIKey"];
        public static string _apiUrlBase = "https://southeastasia.api.cognitive.microsoft.com/vision/v1.0/generateThumbnail";

        [FunctionName("CreateThumbnail")]
        public static void Run([BlobTrigger("image/{name}", Connection = "ImageStorageAccountConnection")]Stream imageBlob, 
                                string name,
                                [Blob("thumbnail/{name}", FileAccess.Write)] Stream imagethumbnail,
                                TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {imageBlob.Length} Bytes");

            if (imageBlob.Length >= 4000000)
            {
                //for large image, create thumbnail using ImageResizer
                var imageResizersettings = new ImageResizer.ResizeSettings
                {
                    MaxHeight = 320,
                    MaxWidth = 320
                };

                ImageResizer.ImageBuilder.Current.Build(imageBlob, imagethumbnail, imageResizersettings);
            }else
            {
                // for small image, using cognative service to create thumbnail

                int width = 320;
                int height = 320;
                bool smartCropping = true;

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_apiUrlBase);
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                    using (HttpContent content = new StreamContent(imageBlob))
                    {
                        //get response
                        content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");
                        var uri = $"{_apiUrlBase}?width={width}&height={height}&smartCropping={smartCropping.ToString()}";
                        var response = httpClient.PostAsync(uri, content).Result;
                        var responseBytes = response.Content.ReadAsByteArrayAsync().Result;

                        //write to output thumb
                        imagethumbnail.Write(responseBytes, 0, responseBytes.Length);
                    }
                }
            }
        }
    }
}

