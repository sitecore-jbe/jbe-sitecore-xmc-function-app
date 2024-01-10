using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sitecore
{
    public class RequestParameters
    {
        public string ContentHubTenantUrl { get; set; }

        public string ContentHubUsername { get; set; }

        public string ContentHubPassword { get; set; }

        public string ContentHubClientId { get; set; }

        public string ContentHubClientSecret { get; set; }

        public string XMCTenantUrl { get; set; }

        public string XMCClientId { get; set; }

        public string XMCClientSecret { get; set; }

        public static RequestParameters Initialize(string requestBody, IQueryCollection requestQuery, ILogger log)
        {
            log.LogInformation("'Sitecore.RequestParameters.Initialize' started.");

            var instance = new RequestParameters
            {
                ContentHubTenantUrl = requestQuery["contentHubTenantUrl"],
                ContentHubUsername = requestQuery["contentHubUsername"],
                ContentHubPassword = requestQuery["contentHubPassword"],
                ContentHubClientId = requestQuery["contentHubClientId"],
                ContentHubClientSecret = requestQuery["contentHubClientSecret"],
                XMCTenantUrl = requestQuery["xmcTenantUrl"],
                XMCClientId = requestQuery["xmcClientId"],
                XMCClientSecret = requestQuery["xmcClientSecret"],
            };

            log.LogInformation("Request query parameter 'ContentHubTenantUrl': " + instance.ContentHubTenantUrl);
            log.LogInformation("Request query parameter 'ContentHubUsername': " + instance.ContentHubUsername);
            log.LogInformation("Request query parameter 'ContentHubPassword': " + instance.ContentHubPassword);
            log.LogInformation("Request query parameter 'ContentHubClientId': " + instance.ContentHubClientId);
            log.LogInformation("Request query parameter 'ContentHubClientSecret': " + instance.ContentHubClientSecret);
            log.LogInformation("Request query parameter 'XMCTenantUrl': " + instance.XMCTenantUrl);
            log.LogInformation("Request query parameter 'XMCClientId': " + instance.XMCClientId);
            log.LogInformation("Request query parameter 'XMCClientSecret': " + instance.XMCClientSecret);

            // Only for debugging.
            if (instance.ContentHubTenantUrl == null)
            {
                instance.ContentHubTenantUrl = "https://almu-llbg.sitecoresandbox.cloud";
                log.LogInformation("Request query parameter 'ContentHubTenantUrl' not provided, using default: " + instance.ContentHubTenantUrl);
            }
            if (instance.ContentHubUsername == null)
            {
                instance.ContentHubUsername = "JBESitecoreXMC";
                log.LogInformation("Request query parameter 'ContentHubUsername' not provided, using default: " + instance.ContentHubUsername);
            }
            if (instance.ContentHubPassword == null)
            {
                instance.ContentHubPassword = "S!t3c0r3";
                log.LogInformation("Request query parameter 'ContentHubPassword' not provided, using default: " + instance.ContentHubPassword);
            }
            if (instance.ContentHubClientId == null)
            {
                instance.ContentHubClientId = "JBESitecoreXMC";
                log.LogInformation("Request query parameter 'ContentHubClientId' not provided, using default: " + instance.ContentHubClientId);
            }
            if (instance.ContentHubClientSecret == null)
            {
                instance.ContentHubClientSecret = "S!t3c0r3";
                log.LogInformation("Request query parameter 'ContentHubClientSecret' not provided, using default: " + instance.ContentHubClientSecret);
            }
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

            log.LogInformation("'Sitecore.RequestParameters.Initialize' ended.");
            return instance;
        }
    }
}
