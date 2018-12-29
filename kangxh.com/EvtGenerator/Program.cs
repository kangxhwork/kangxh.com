using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;

namespace EvtGenerator
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976

    public class elkEvent
    {

        public DateTime time { get; set; } 
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        // this is the demo of https://blogs.msdn.microsoft.com/atverma/2018/09/24/azure-kubernetes-service-aks-deploying-elasticsearch-logstash-and-kibana-elk-and-consume-messages-from-azure-event-hub/

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static string connectionString = "Endpoint=sb://kangxhevthubsea.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kJhvWFVnHvqxF21mhUPrMlyKmcvY6mAJWWvEbOhHFBg=;EntityPath=akselk";

        static void Main()
        {
            Console.WriteLine("Starting sending messages to Azure event hub");

            while (true)
            {
                AsyncSendingRandomMessages();
                Thread.Sleep(30000);
            }
            
        }

        static async void AsyncSendingRandomMessages()
        {
            await SendingRandomMessages();
        }
        async static Task SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
            Random random = new Random(1);

            try
            {
                var message = JsonConvert.SerializeObject(new elkEvent() { time = DateTime.Now, ID = random.Next(), Name = $"Sample Name {random.Next()}" });
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                Console.ResetColor();
            }
        }
    }
}
