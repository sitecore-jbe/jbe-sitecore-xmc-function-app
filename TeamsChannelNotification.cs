using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Sitecore.XMC.WebhookRequest;

namespace Sitecore
{
    public static class TeamsChannelNotification
    {
        [FunctionName("TeamsChannelNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //Structure Request Parameters
            var requestParameters = TeamsChannelNotificationRequestParameters.Initialize(requestBody, req.Query, log);

            //Structure Webhook Event
            var webHookEvent = WebHookEvent.Initialize(requestBody, log);

            var adaptiveCardContent = await webHookEvent.GetMicrosoftTeamsAdaptiveCardContent(
                requestParameters.XMCTenantName,
                requestParameters.XMCTenantUrl,
                requestParameters.XMCApiKey,
                log
                );

            var response = await Microsoft.Teams.Teams.PostTeamsChannelNotification(requestParameters.TeamsWebHookUrl, adaptiveCardContent, log);

            string responseMessage = "This HTTP triggered function executed successfully with the following response: \r\n" + response.Content.ToString();

            return new OkObjectResult(responseMessage);
        }

    }
}
