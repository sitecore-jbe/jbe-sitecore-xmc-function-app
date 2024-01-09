using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class OAuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public static async Task<OAuthToken> GetOAuthTokenAsync(string clientId, string clientSecret, ILogger log)
        {
            log.LogInformation("Function 'Sitecore.XMC.OAuthToken.GetOAuthTokenAsync' started.");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://auth.sitecorecloud.io/oauth/token");

            var collection = new List<KeyValuePair<string, string>>
            {
                new("client_id", clientId),
                new("client_secret", clientSecret),
                new("audience", "https://api.sitecorecloud.io"),
                new("grant_type", "client_credentials")
            };

            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            OAuthToken oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(response.Content.ReadAsStringAsync().Result);
            log.LogInformation("Function 'Sitecore.XMC.OAuthToken.GetOAuthTokenAsync' ended.");

            return oAuthToken;
        }

    }
}
