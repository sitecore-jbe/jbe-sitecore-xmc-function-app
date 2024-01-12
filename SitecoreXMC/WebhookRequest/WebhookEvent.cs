using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;

namespace Sitecore.XMC.WebhookRequest
{
    public class WebHookEvent
    {
        [JsonProperty("EventName")]
        public string EventName { get; set; }

        public async Task<Microsoft.Teams.AdaptiveCardContent> GetMicrosoftTeamsAdaptiveCardContent(string tenantUrl, string apiKey, ILogger log)
        {
            var adaptiveCardContent = new Microsoft.Teams.AdaptiveCardContent();

            switch (EventName)
            {
                case "item:added":
                    adaptiveCardContent.Title = "A new item from a template is added.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:versionAdded":
                    adaptiveCardContent.Title = "A new version is added to the item.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:cloneAdded":
                    adaptiveCardContent.Title = "A new cloned item is added";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:deleted":
                    adaptiveCardContent.Title = "the item is deleted";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:deleting":
                    adaptiveCardContent.Title = "Raised before deleting the item.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:versionRemoved":
                    adaptiveCardContent.Title = "Raised when a version of the item is removed.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:locked":
                    adaptiveCardContent.Title = "Raised when the item is locked.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:unlocked":
                    adaptiveCardContent.Title = "Raised when the item is unlocked.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:saved":
                    adaptiveCardContent.Title = "Item saved.";
                    adaptiveCardContent.Description = "The item **" + Item.Name + "** was saved. \r The following field values have changed:";
                    foreach (var x in Changes.FieldChanges)
                    {
                        var fieldName = await XMC.Item.GetFieldNameAsync(
                            tenantUrl,
                            apiKey,
                            x.FieldId,
                            Item.Id,
                            log);
                        adaptiveCardContent.Fields.Add("The value of the field **" + fieldName + "** has changed from **" + x.OriginalValue.Replace("\\", "\\\\") + "** to **" + x.Value.Replace("\\", "\\\\") + "**");
                    }
                    break;
                case "item:copied":
                    adaptiveCardContent.Title = "The item " + Item.Name + " is copied";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:moved":
                    adaptiveCardContent.Title = "The item " + Item.Name + " is moved to another parent.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:renamed":
                    adaptiveCardContent.Title = "The item " + Item.Name + " is renamed.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:sortorderchanged":
                    adaptiveCardContent.Title = "The sort order of the item " + Item.Name + " changed.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "item:templateChanged":
                    adaptiveCardContent.Title = "The template of the item " + Item.Name + " has changed.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "publish:begin":
                    adaptiveCardContent.Title = "Raised when the publishing of the item starts.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "publish:end":
                    adaptiveCardContent.Title = "Raised when the publishing of the item ends.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "publish:fail":
                    adaptiveCardContent.Title = "Raised when the publishing of the item fails.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                case "publish:statusUpdated":
                    adaptiveCardContent.Title = "Raised when the status of item publishing updates.";
                    adaptiveCardContent.Description = adaptiveCardContent.Title;
                    break;
                default: return null;
            }

            return adaptiveCardContent;
        }

        [JsonProperty("Item")]
        public Item Item { get; set; }

        [JsonProperty("Changes")]
        public Changes Changes { get; set; }

        [JsonProperty("WebhookItemId")]
        public Guid WebhookItemId { get; set; }

        [JsonProperty("WebhookItemName")]
        public string WebhookItemName { get; set; }

        public static WebHookEvent Initialize(string requestBody, ILogger log)
        {
            log.LogInformation("'Sitecore.XMC.WebhookRequest.WebhookEvent.Initialize' started.");

            WebHookEvent webhookEvent = JsonConvert.DeserializeObject<WebHookEvent>(requestBody);

            log.LogInformation("'Sitecore.XMC.WebhookRequest.WebhookEvent.Initialize' ended.");

            return webhookEvent;
        }
    }
}
