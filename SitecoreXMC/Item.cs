using System;
using System.Linq;
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
        public static async Task<GraphQL.UpdateItem.Result> UpdateItemWithContentHubURLAsync(string tenantUrl, string oAuthToken, Guid itemId, string rawValue, ILogger log)
        {
            log.LogInformation("'Sitecore.XMC.Item.UpdateItemWithContentHubURLAsync' started.");

            var client = new HttpClient();
            HttpRequestMessage request = tenantUrl.EndsWith("/")
                ? new HttpRequestMessage(HttpMethod.Post, tenantUrl + "sitecore/api/authoring/graphql/v1/")
                : new HttpRequestMessage(HttpMethod.Post, tenantUrl + "/sitecore/api/authoring/graphql/v1/");
            request.Headers.Add("Authorization", "Bearer " + oAuthToken);

            var content = new StringContent("{\"query\":\"mutation {\\r\\n  updateItem(\\r\\n    input: {\\r\\n      itemId: \\\"{" + itemId.ToString() + "}\\\"\\r\\n      fields: [\\r\\n        { name: \\\"ContentHubURL\\\", value: \\\"" + rawValue + "\\\" }\\r\\n      ]\\r\\n    }\\r\\n  ) {\\r\\n    item {\\r\\n      itemId\\r\\n    }\\r\\n  }\\r\\n}\",\"variables\":{}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            GraphQL.UpdateItem.Result item = JsonConvert.DeserializeObject<GraphQL.UpdateItem.Result>(response.Content.ReadAsStringAsync().Result);
            log.LogInformation("'Sitecore.XMC.Item.UpdateItemWithContentHubURLAsync' ended.");

            return item;
        }

        public static async Task<string> GetFieldNameAsync(string tenantUrl, string apiKey, Guid fieldId, Guid itemId, ILogger log)
        {
            log.LogInformation("'Sitecore.XMC.Item.GetItemAsync' started.");

            //Preview API Key needs to be configured: https://doc.sitecore.com/xmc/en/developers/xm-cloud/set-up-the-graphql-playgrounds-to-test-published-content.html

            //url = https://xmc-sitecoresaacf82-jbexmclouddemo-production.sitecorecloud.io/

            var client = new HttpClient();
            HttpRequestMessage request = tenantUrl.EndsWith("/")
                ? new HttpRequestMessage(HttpMethod.Post, tenantUrl + "sitecore/api/graph/edge")
                : new HttpRequestMessage(HttpMethod.Post, tenantUrl + "/sitecore/api/graph/edge");
            request.Headers.Add("sc_apikey", apiKey);

            var content = new StringContent($@"
                {{
                    ""query"":""query {{
                        item(path: \""{itemId}\"", language: \""en\"") {{
                            fields {{
                                id
                                definition {{
                                    name
                                    title
                                }}
                            }}
                        }}
                    }}"",
                    ""variables"":{{
                    }}
                }}", null, "application/json");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            GraphQL.GetFieldName.Result result = JsonConvert.DeserializeObject<GraphQL.GetFieldName.Result>(response.Content.ReadAsStringAsync().Result);

            string fieldName;
            switch (fieldId.ToString())
            {
                case "8cdc337e-a112-42fb-bbb4-4143751e123f":
                    fieldName = "Revision";
                    break;
                case "badd9cf9-53e0-4d0c-bcc0-2d784c282f6a":
                    fieldName = "Updated by";
                    break;
                default:
                    var field = result.Data.Item.Fields.Single(e => e.Id == fieldId.ToString().ToUpper().Replace("-", ""));
                    fieldName = field.Definition.Name.StartsWith("__") ? field.Definition.Title : field.Definition.Name;
                    break;
            }

            log.LogInformation("'Sitecore.XMC.Item.GetItemAsync' ended.");

            return fieldName;
        }

    }
}