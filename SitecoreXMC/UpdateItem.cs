using System;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sitecore.XMC.GraphQL;

namespace Sitecore.XMC
{
    public class Item
    {
        public static async Task<UpdateItemResult> UpdateItemAsync(string tenantUrl, string oAuthToken, Guid itemId, string rawValue, ILogger log)
        {
            log.LogInformation("Function 'Sitecore.XMC.Item.UpdateItemAsync' started.");

            var client = new HttpClient();
            HttpRequestMessage request = tenantUrl.EndsWith("/")
                ? new HttpRequestMessage(HttpMethod.Post, tenantUrl + "sitecore/api/authoring/graphql/v1/")
                : new HttpRequestMessage(HttpMethod.Post, tenantUrl + "/sitecore/api/authoring/graphql/v1/");
            request.Headers.Add("Authorization", "Bearer " + oAuthToken);

            var content = new StringContent("{\"query\":\"mutation {\\r\\n  updateItem(\\r\\n    input: {\\r\\n      itemId: \\\"{" + itemId.ToString() + "}\\\"\\r\\n      fields: [\\r\\n        { name: \\\"ContentHubURL\\\", value: \\\"" + rawValue + "\\\" }\\r\\n      ]\\r\\n    }\\r\\n  ) {\\r\\n    item {\\r\\n      itemId\\r\\n    }\\r\\n  }\\r\\n}\",\"variables\":{}}", null, "application/json");
            //var content = new StringContent($"{{\"query\":\"mutation {{\\r\\n  updateItem(\\r\\n    input: {{\\r\\n      itemId: \\\"{" + itemId.ToString() + "}\\\"\\r\\n      fields: [\\r\\n        {{ name: \\\"ContentHubURL\\\", value: \\\"" + rawValue + "\\\" }}\\r\\n      ]\\r\\n    }}\\r\\n  ) {{\\r\\n    item {{\\r\\n      itemId\\r\\n    }}\\r\\n  }}\\r\\n}}\",\"variables\":{{}}}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            UpdateItemResult item = JsonConvert.DeserializeObject<UpdateItemResult>(response.Content.ReadAsStringAsync().Result);
            log.LogInformation("Function 'Sitecore.XMC.Item.UpdateItemAsync' ended.");

            return item;
        }

    }
}