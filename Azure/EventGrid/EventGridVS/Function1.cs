// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure.Storage.Blobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EventGridVS
{
    public static class Function1
    {
        //[FunctionName("Function1")]
        //public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        //{
        //    log.LogInformation(eventGridEvent.Data.ToString());
        //}
        [FunctionName("EventGridVSCode")]
        public static async Task Run([EventGridTrigger] EventGridEvent gridEvent,
           [Blob("{blob.url}", FileAccess.Read)] Stream inputBlobStream,
            ILogger log)
        {
            log.LogInformation($"Subject: {gridEvent.Subject}");

            try
            {
                var createdBlobInfo = ((JObject)gridEvent.Data).ToObject<StorageBlobCreatedEventData>();

                var blobName = (new BlobClient(new Uri(createdBlobInfo.Url))).Name;

                var outputBlobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));

                var outputBlobContainerClient = outputBlobServiceClient.GetBlobContainerClient("greyscale");

                using (var image = Image.Load(inputBlobStream))
                {
                    using (var outputBlobStream = new MemoryStream())
                    {
                        image.Mutate(x => x.Grayscale());

                        await image.SaveAsPngAsync(outputBlobStream);

                        outputBlobStream.Position = 0;

                        // save to Azure storage
                        await outputBlobContainerClient.UploadBlobAsync(blobName, outputBlobStream);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "can not convert");
            }
        }
    }
}
