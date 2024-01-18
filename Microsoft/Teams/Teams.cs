using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AdaptiveCards;
using AdaptiveCards.Templating;
using Microsoft.Azure.WebJobs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Connector;
using System.Text.RegularExpressions;
using System.Net.Mime;


namespace Microsoft.Teams
{
    public class Teams
    {
        public static string RemoveNewLines(string s)
        {
            return s.Replace("\r", "").Replace("\n", "");
        }

        public static async Task<System.Net.Http.HttpResponseMessage> PostTeamsChannelNotificationBad(string webhookUrl, AdaptiveCardData adaptiveCardData, ILogger log, ExecutionContext context)
        {
            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' started.");
            string[] paths = { "Cards", "XMCNotification.json" };

            // Load the Adaptive Card template from a file.
            string templatePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(context.FunctionAppDirectory).ToString()).ToString()) + "\\" + Path.Combine(paths);
            string adaptiveCardTemplate = File.ReadAllText(templatePath);

            // Parse the Adaptive Card template.
            var cardTemplate = new AdaptiveCardTemplate(adaptiveCardTemplate);

            // Replace placeholders in the template with data from the context.
            var adaptiveCard = cardTemplate.Expand(adaptiveCardData.ToJson());

            // Create a Message with the Adaptive Card.
            var message = new
            {
                type = "message",
                attachments = new
                {
                    ContentType = AdaptiveCard.ContentType,
                    ContentUrl = "",
                    Content = JsonConvert.DeserializeObject(adaptiveCard),
                }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(message), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookUrl, content);
            response.EnsureSuccessStatusCode();

            log.LogInformation("'Microsoft.Teams.Teams.PostTeamsChannelNotification' ended.");

            return response;


        }

        public static async Task<System.Net.Http.HttpResponseMessage> PostTeamsChannelNotification(string webhookUrl, AdaptiveCardData adaptiveCardContent, ILogger log)
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