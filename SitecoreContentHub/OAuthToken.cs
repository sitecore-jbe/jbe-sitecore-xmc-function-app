using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sitecore.ContentHub
{
    public class OAuthToken
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public Guid RefreshToken { get; set; }

        public static async Task<OAuthToken> GetOAuthTokenAsync(string tenantUrl, string username, string password, string clientId, string clientSecret, ILogger log)
        {
            log.LogInformation("Function 'Sitecore.ContentHub.OAuthToken.GetOAuthToken' started.");

            var client = new HttpClient();
            HttpRequestMessage request = tenantUrl.EndsWith("/")
                ? new HttpRequestMessage(HttpMethod.Post, tenantUrl + "oauth/token")
                : new HttpRequestMessage(HttpMethod.Post, tenantUrl + "/oauth/token");

            var collection = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "password"),
                new("username", username),
                new("password", password),
                new("client_id", clientId),
                new("client_secret", clientSecret)
            };

            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            OAuthToken oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(response.Content.ReadAsStringAsync().Result);
            log.LogInformation("Function 'Sitecore.ContentHub.OAuthToken.GetOAuthToken' ended.");

            return oAuthToken;
        }

    }
}
