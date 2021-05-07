using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubSender
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://myeventhubs01.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xOu182oVX5QSu7gCptRpljS2ukXB8P6/u0Dlc2Bwta8=";
        private const string eventHubName = "eventhub02";

        static async Task Main()
        {
            // Create a producer client that you can use to send events to an event hub
            //await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            await using var producerClient = new EventHubProducerClient(connectionString, eventHubName);
            
                // Create a batch of events 
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
            //eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("First event")));
            //eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second event")));
            //eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third event")));

            string message ="{\"id\":\"device3\", \"timestamp\":\"2021-05-17T01:17:00Z\"}";
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message)));

            // Use the producer client to send the batch of events to the event hub
            await producerClient.SendAsync(eventBatch);

            Console.WriteLine("A batch of event has been published.");
            
        }
    }
}
