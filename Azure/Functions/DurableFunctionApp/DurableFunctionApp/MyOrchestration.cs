using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DurableFunctionApp
{
	public static class MyOrchestration
	{
		[FunctionName("MyOrchestration")]
		public static async Task<List<string>> RunOrchestrator(
			[OrchestrationTrigger] IDurableOrchestrationContext context)
		{
			var outputs = new List<string>
			{
				// Replace "hello" with the name of your Durable Activity Function.
				await context.CallActivityAsync<string>("MyOrchestration_Hello", "Tokyo"),
				await context.CallActivityAsync<string>("MyOrchestration_Hello", "Seattle"),
				await context.CallActivityAsync<string>("MyOrchestration_Hello", "London")
			};

			// returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
			return outputs;
		}

		[FunctionName("MyOrchestration_Hello")]
		public static string SayHello([ActivityTrigger] string name, ILogger log)
		{
			log.LogInformation($"Saying hello to {name}.");
			return $"Hello {name}!";
		}

		[FunctionName("MyOrchestration_HttpStart")]
		public static async Task<HttpResponseMessage> HttpStart(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
			[DurableClient] IDurableOrchestrationClient starter,
			ILogger log)
		{
			// Function input comes from the request content.
			var instanceId = await starter.StartNewAsync("MyOrchestration", null);

			log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

			return starter.CreateCheckStatusResponse(req, instanceId);
		}
	}
}
