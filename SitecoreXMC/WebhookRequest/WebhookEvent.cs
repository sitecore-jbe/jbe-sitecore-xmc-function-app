using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class WebHookEvent
    {
        [JsonProperty("EventName")]
        public string EventName { get; set; }

        [JsonProperty("Item")]
        public Item Item { get; set; }

        [JsonProperty("Changes")]
        public Changes Changes { get; set; }

        [JsonProperty("WebhookItemId")]
        public Guid WebhookItemId { get; set; }

        [JsonProperty("WebhookItemName")]
        public string WebhookItemName { get; set; }

        public static WebHookEvent Initialize(string requestBody, ILogger log)
        {
            log.LogInformation("Function 'Sitecore.XMC.ContentHub.WebhookEvent.Initialize' started.");

            WebHookEvent webhookEvent = JsonConvert.DeserializeObject<WebHookEvent>(requestBody);

            log.LogInformation("Function 'Sitecore.XMC.ContentHub.WebhookEvent.Initialize' ended.");

            return webhookEvent;
        }
    }
}
