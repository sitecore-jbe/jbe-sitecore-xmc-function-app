using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Sitecore
{
    public class ShareOnTeamsChannelRequestParameters
    {
        public string TeamsWebHookUrl { get; set; }

        public string XMCTenantUrl { get; set; }

        public string XMCApiKey { get; set; }

        public string XMCClientId { get; set; }

        public string XMCClientSecret { get; set; }

        public static ShareOnTeamsChannelRequestParameters Initialize(string requestBody, IQueryCollection requestQuery, ILogger log)
        {
            log.LogInformation("'Sitecore.ShareOnTeamsChannelRequestParameters.Initialize' started.");

            var instance = new ShareOnTeamsChannelRequestParameters
            {
                TeamsWebHookUrl = requestQuery["teamsWebHookUrl"],
                XMCTenantUrl = requestQuery["xmcTenantUrl"],
                XMCClientId = requestQuery["xmcClientId"],
                XMCClientSecret = requestQuery["xmcClientSecret"],
                XMCApiKey = requestQuery["xmcApiKey"],
            };

            log.LogInformation("Request query parameter 'TeamsWebHookUrl': " + instance.TeamsWebHookUrl);
            log.LogInformation("Request query parameter 'XMCApiKey': " + instance.XMCApiKey);
            log.LogInformation("Request query parameter 'XMCTenantUrl': " + instance.XMCTenantUrl);
            log.LogInformation("Request query parameter 'XMCClientId': " + instance.XMCClientId);
            log.LogInformation("Request query parameter 'XMCClientSecret': " + instance.XMCClientSecret);

            // Only for debugging.
            if (instance.XMCTenantUrl == null)
            {
                instance.XMCTenantUrl = "https://xmc-sitecoresaacf82-jbexmclouddemo-production.sitecorecloud.io";
                log.LogInformation("Request query parameter 'XMCTenantUrl' not provided, using default: " + instance.XMCTenantUrl);
            }
            if (instance.XMCClientId == null)
            {
                instance.XMCClientId = "xfOoiiLN0Z4UCcr63HaSAcwG52z7INzN";
                log.LogInformation("Request query parameter 'XMCClientId' not provided, using default: " + instance.XMCClientId);
            }
            if (instance.XMCClientSecret == null)
            {
                instance.XMCClientSecret = "mohNocAmpvzgG5I773pwYMfyG3bh8XH7_8ARaDKkyCw_LDtHDz8lItoqPuy1WJQH";
                log.LogInformation("Request query parameter 'XMCClientSecret' not provided, using default: " + instance.XMCClientSecret);
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

            log.LogInformation("'Sitecore.ShareOnTeamsChannelRequestParameters.Initialize' ended.");
            return instance;
        }
    }
}
