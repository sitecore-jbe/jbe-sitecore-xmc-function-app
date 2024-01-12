using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Sitecore
{
    public class TeamsChannelNotificationRequestParameters
    {
        public string TeamsWebHookUrl { get; set; }

        public string XMCTenantUrl { get; set; }

        public string XMCApiKey { get; set; }

        public static TeamsChannelNotificationRequestParameters Initialize(string requestBody, IQueryCollection requestQuery, ILogger log)
        {
            log.LogInformation("'Sitecore.TeamsChannelNotificationRequestParameters.Initialize' started.");

            var instance = new TeamsChannelNotificationRequestParameters
            {
                TeamsWebHookUrl = requestQuery["teamsWebHookUrl"],
                XMCTenantUrl = requestQuery["xmcTenantUrl"],
                XMCApiKey = requestQuery["xmcApiKey"],
            };

            log.LogInformation("Request query parameter 'TeamsWebHookUrl': " + instance.TeamsWebHookUrl);
            log.LogInformation("Request query parameter 'XMCApiKey': " + instance.XMCApiKey);
            log.LogInformation("Request query parameter 'XMCTenantUrl': " + instance.XMCTenantUrl);

            // Only used for development.
            if (instance.XMCTenantUrl == null)
            {
                instance.XMCTenantUrl = "https://xmc-sitecoresaacf82-jbexmclouddemo-production.sitecorecloud.io";
                log.LogInformation("Request query parameter 'XMCTenantUrl' not provided, using default: " + instance.XMCTenantUrl);
            }
            if (instance.XMCApiKey == null)
            {
                instance.XMCApiKey = "29CD9D3D-802E-4CDF-98A6-05F91C527C79";
                log.LogInformation("Request query parameter 'XMCApiKey' not provided, using default: " + instance.XMCApiKey);
            }
            if (instance.TeamsWebHookUrl == null)
            {
                instance.TeamsWebHookUrl = "https://sitecore1.webhook.office.com/webhookb2/a4da760e-39c8-4987-bd4a-31c902ef4fcf@91700184-c314-4dc9-bb7e-a411df456a1e/IncomingWebhook/b8edd370d8054592a8ca7ebee0eb6754/2ce4b16e-da6d-4c29-b4a6-115baafa88cc";
                log.LogInformation("Request query parameter 'TeamsWebHookUrl' not provided, using default: " + instance.TeamsWebHookUrl);
            }

            log.LogInformation("'Sitecore.TeamsChannelNotificationRequestParameters.Initialize' ended.");
            return instance;
        }
    }
}
