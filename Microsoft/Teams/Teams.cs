using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sitecore.XMC.GraphQL;

namespace Microsoft.Teams
{
    public class Teams
    {
        public static async Task<System.Net.Http.HttpResponseMessage> PostTeamsChannelNotification(string webhookUrl, AdaptiveCardContent adaptiveCardContent, ILogger log)
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
                            ""title"": ""Suggested Actions"",
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