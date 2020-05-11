using System;
using System.Data.Common;
using System.Text;
using Azure.Messaging;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHub___sender
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://vishaleventhub2.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vJGFV5DaKLcCsZRSdxIkj767MdqYfncTH9PFr3M2V+g=";
        private const string eventHubName = "vishaleventhub";
        static void Main(string[] args)
        {
            Console.WriteLine("Sending Message asyncronically...");
            sendMessageAsync();
            Console.ReadLine();
        }

        private static async void sendMessageAsync()
        { 
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                while(true)
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Event Data: " + 1234 +System.DateTime.Now.ToString())));
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
                    await producerClient.SendAsync(eventBatch);
                    Console.WriteLine("A batch of 2 events has been published.");
                    Console.ReadLine();

                }
            }
        }
    }
}
