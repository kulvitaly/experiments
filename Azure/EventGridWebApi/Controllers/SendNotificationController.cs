using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventGridWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendNotificationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SendNotificationController> _logger;

        public SendNotificationController(ILogger<SendNotificationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private bool  EventTypeSubscriptionValidation => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() == "SubscriptionValidation";
        private bool  EventTypeNotification => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() == "Notification";
        
        
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using var requestStream = new StreamReader(Request.Body);

            var bodyJson = await requestStream.ReadToEndAsync();

            var events = JsonConvert.DeserializeObject<List<EventGridEvent>>(bodyJson);

            if (EventTypeSubscriptionValidation)
            {
                var subValidationEventData = ((JObject)events.First().Data).ToObject<SubscriptionValidationEventData>();

                return new OkObjectResult(new SubscriptionValidationResponse(subValidationEventData.ValidationCode));
            }

            if (EventTypeNotification)
            {
                var notificationEvent = events.First();
                _logger.LogInformation(notificationEvent.Subject);

                return new OkResult();
            }

            return null;
        }
    }
}
