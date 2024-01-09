using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class RequestParameters
    {
        public string TenantUrl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ContentHubClientId { get; set; }

        public string ContentHubClientSecret { get; set; }

        public string EntityIdentifier { get; set; }

        public string XMCTenantUrl { get; set; }

        public string XMCClientId { get; set; }

        public string XMCClientSecret { get; set; }

        public static RequestParameters Initialize(string requestBody, IQueryCollection requestQuery, ILogger log)
        {
            var instance = new RequestParameters
            {
                TenantUrl = requestQuery["tenantUrl"],
                Username = requestQuery["username"],
                Password = requestQuery["password"],
                ContentHubClientId = requestQuery["contenthubclientId"],
                ContentHubClientSecret = requestQuery["contenthubclientSecret"],
                EntityIdentifier = requestQuery["entityIdentifier"],
                XMCTenantUrl = requestQuery["xmcTenantUrl"],
                XMCClientId = requestQuery["xmcClientId"],
                XMCClientSecret = requestQuery["xmcClientSecret"],
            };

            // Only for debugging.
            if (instance.TenantUrl == null) { instance.TenantUrl = "https://almu-llbg.sitecoresandbox.cloud"; }
            if (instance.Username == null) { instance.Username = "JBESitecoreXMC"; }
            if (instance.Password == null) { instance.Password = "S!t3c0r3"; }
            if (instance.ContentHubClientId == null) { instance.ContentHubClientId = "JBESitecoreXMC"; }
            if (instance.ContentHubClientSecret == null) { instance.ContentHubClientSecret = "S!t3c0r3"; }
            if (instance.EntityIdentifier == null) { instance.EntityIdentifier = "Gs8f4QkTs0WEyYCdT-GPAg"; }
            if (instance.XMCTenantUrl == null) { instance.XMCTenantUrl = "https://xmc-sitecoresaacf82-jbexmclouddemo-production.sitecorecloud.io"; }
            if (instance.XMCClientId == null) { instance.XMCClientId = "xfOoiiLN0Z4UCcr63HaSAcwG52z7INzN"; }
            if (instance.XMCClientSecret == null) { instance.XMCClientSecret = "mohNocAmpvzgG5I773pwYMfyG3bh8XH7_8ARaDKkyCw_LDtHDz8lItoqPuy1WJQH"; }

            return instance;
        }
    }
}
