namespace Sitecore.XMC
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class EntityResource
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        public static async Task<EntityResource> GetEntityResourceAsync(string tenantUrl, string oAuthToken, string entityIdentifier, ILogger log)
        {
            log.LogInformation("Function 'Sitecore.XMC.ContentHub.EntityResource.GetEntityResourceAsync' started.");

            var client = new HttpClient();
            HttpRequestMessage request = tenantUrl.EndsWith("/")
                ? new HttpRequestMessage(HttpMethod.Get, tenantUrl + "api/entities/identifier/" + entityIdentifier)
                : new HttpRequestMessage(HttpMethod.Get, tenantUrl + "/api/entities/identifier/" + entityIdentifier);

            request.Headers.Add("Authorization", "Bearer " + oAuthToken);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            EntityResource entityResource = JsonConvert.DeserializeObject<EntityResource>(response.Content.ReadAsStringAsync().Result);
            log.LogInformation("Function 'Sitecore.XMC.ContentHub.EntityResource.GetEntityResourceAsync' ended.");

            return entityResource;
        }
    }
}
