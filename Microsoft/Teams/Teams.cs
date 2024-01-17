using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AdaptiveCards;
using AdaptiveCards.Templating;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microsoft.Teams
{
    public class Teams
    {
        public static async Task<System.Net.Http.HttpResponseMessage> PostTeamsChannelNotification(string webhookUrl, AdaptiveCardContent adaptiveCardData, ILogger log, ExecutionContext Context)
        {
            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' started.");

            string[] path = { "Cards", "XMCNotification.json" };

            try
            {
                var adaptiveCardTemplateJson = File.ReadAllText(Context.FunctionAppDirectory + Path.Combine(path));

                var getClient = new HttpClient();


                getClient.BaseAddress = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME").Contains("localhost")
                    ? new Uri(Environment.GetEnvironmentVariable("PublishedUrl"))
                    : new Uri(Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME"));
                getClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var adaptiveCardTemplateJson = await getClient.GetStringAsync("/Cards/XMCNotification.json");
                var adaptiveCardTemplate = new AdaptiveCardTemplate(adaptiveCardTemplateJson);




                //"Expand" the template -this generates the final Adaptive Card payload
                var expandedAdaptiveCardTemplate = adaptiveCardTemplate.Expand(JsonConvert.SerializeObject(adaptiveCardData).ToString());
                var adaptiveCardStringContent = new StringContent(JsonConvert.DeserializeObject(expandedAdaptiveCardTemplate).ToString(), System.Text.Encoding.UTF8, "application/vnd.microsoft.card.adaptive");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync(webhookUrl, adaptiveCardStringContent);
                response.EnsureSuccessStatusCode();
                return response;

            }
            catch (AdaptiveSerializationException ex)
            {
                // Failed to deserialize card 
                // This occurs from malformed JSON
                // or schema violations like required properties missing 
            }

            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' ended.");
            return null;
        }


        public static async Task<System.Net.Http.HttpResponseMessage> PostTeamsChannelNotificationGood(string webhookUrl, AdaptiveCardContent adaptiveCardContent, ILogger log)
        {
            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' started.");

            var adaptiveCardContentFields = "";
            var fieldCounter = 0;

            foreach (var field in adaptiveCardContent.Fields)
            {
                fieldCounter++;
                adaptiveCardContentFields += (fieldCounter == 1)
                                             ? "- " + field
                                             : "\r- " + field;
            }

            var adaptiveCardJson = $@"{{
    ""type"": ""message"",
    ""attachments"": [
        {{
            ""contentType"": ""application/vnd.microsoft.card.adaptive"",
            ""contentUrl"": null,
            ""content"": {{
                ""type"": ""AdaptiveCard"",
                ""msteams"": {{
                    ""width"": ""Full""
                }},
                ""body"": [
                    {{
                        ""type"": ""TextBlock"",
                        ""text"": ""{adaptiveCardContent.Title}"",
                        ""style"": ""heading"",
                        ""wrap"": true
                    }},
                    {{
                        ""type"": ""TextBlock"",
                        ""text"": ""{adaptiveCardContent.Description}"",
                        ""wrap"": true
                    }},
                    {{
                        ""type"": ""Container"",
                        ""items"": [
                            {{
                                ""type"": ""TextBlock"",
                                ""text"": ""{adaptiveCardContentFields}"",
                                ""wrap"": true
                            }}
                        ]
                    }},
                    {{
                        ""type"": ""ActionSet"",
                        ""actions"": [
                            {{
                                ""type"": ""Action.Execute"",
                                ""title"": ""Open Content Editor"",
                                ""verb"": ""personalDetailsFormSubmit"",
                                ""fallback"": {{
                                    ""type"": ""Action.Submit"",
                                    ""title"": ""Submit""
                                }}  
                            }}
                        ]
                    }}
                ],
                ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                ""version"": ""1.5""
            }}
        }}
    ]
}}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(adaptiveCardJson.Replace("\r\n", ""), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookUrl, content);
            response.EnsureSuccessStatusCode();

            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' ended.");

            return response;
        }
    }
}