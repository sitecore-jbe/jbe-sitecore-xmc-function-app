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

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string EntityIdentifier { get; set; }

        public static RequestParameters Initialize(string requestBody, IQueryCollection requestQuery, ILogger log)
        {
            var instance = new RequestParameters
            {
                TenantUrl = requestQuery["tenantUrl"],
                Username = requestQuery["username"],
                Password = requestQuery["password"],
                ClientId = requestQuery["clientId"],
                ClientSecret = requestQuery["clientSecret"],
                EntityIdentifier = requestQuery["entityIdentifier"]
            };

            // Only for debugging.
            if (instance.TenantUrl == null) { instance.TenantUrl = "https://almu-llbg.sitecoresandbox.cloud"; }
            if (instance.Username == null) { instance.Username = "JBESitecoreXMC"; }
            if (instance.Password == null) { instance.Password = "S!t3c0r3"; }
            if (instance.ClientId == null) { instance.ClientId = "JBESitecoreXMC"; }
            if (instance.ClientSecret == null) { instance.ClientSecret = "S!t3c0r3"; }
            if (instance.EntityIdentifier == null) { instance.EntityIdentifier = "Gs8f4QkTs0WEyYCdT-GPAg"; }

            return instance;
        }
    }
}
