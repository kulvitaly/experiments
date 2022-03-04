using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpoint = "https://evgt-account-created.eastus-1.eventgrid.azure.net/api/events";
            var key = "Xb2CJvQAwU6kt86HwslHSjtXEPWgHkk0Q5eIr02FTgE=";
            var topicHostName = new Uri(endpoint).Host;

            var acct = new EventGridEvent(
                id: Guid.NewGuid().ToString(),
                subject: "New account",
                data: new {Message = "hi"},
                eventType: "NewAccountCreated",
                eventTime: DateTime.Now,
                dataVersion: "1.0");
            
            var credentials = new TopicCredentials(key);

            var client = new EventGridClient(credentials);

            await client.PublishEventsAsync(topicHostName, new EventGridEvent[] { acct });
        }
    }
}
