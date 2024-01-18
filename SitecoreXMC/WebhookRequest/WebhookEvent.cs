using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Teams;
using Newtonsoft.Json;

namespace Sitecore.XMC.WebhookRequest
{
    public class WebHookEvent
    {
        const string lockFieldId = "001dd393-96c5-490b-924a-b0f25cd9efd8";

        [JsonProperty("EventName")]
        public string EventName { get; set; }

        public async Task<Microsoft.Teams.AdaptiveCardData> GetMicrosoftTeamsadaptiveCardData(string tenantName, string tenantUrl, string apiKey, ILogger log)
        {
            var adaptiveCardData = new Microsoft.Teams.AdaptiveCardData();

            switch (EventName)
            {
                case "item:added":
                    adaptiveCardData.Title = "A new item from a template is added.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:versionAdded":
                    adaptiveCardData.Title = "A new version is added to the item.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:cloneAdded":
                    adaptiveCardData.Title = "A new cloned item is added";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:deleted":
                    adaptiveCardData.Title = "the item is deleted";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:deleting":
                    adaptiveCardData.Title = "Raised before deleting the item.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:versionRemoved":
                    adaptiveCardData.Title = "Raised when a version of the item is removed.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:locked":
                    //Get the owner and date.
                    var lockField = Item.VersionedFields.Single(e => e.Id == Guid.Parse(lockFieldId));
                    var doc = new XmlDocument();
                    doc.LoadXml(lockField.Value);
                    var lockOwner = doc.DocumentElement.Attributes["owner"].Value.Replace("\\", "");

                    //adaptiveCardData.Title = "The item **" + Item.Name + "** is locked.";
                    adaptiveCardData.Description = "The **" + Item.Name + "** item is locked by **" + lockOwner + "** on the [jbexmclouddemo](" + tenantUrl + ") tenant.";
                    break;
                case "item:unlocked":
                    //adaptiveCardData.Title = "The item **" + Item.Name + "** is unlocked.";
                    adaptiveCardData.Description = "The **" + Item.Name + "** item is unlocked on the [jbexmclouddemo](" + tenantUrl + ") tenant.";
                    break;
                case "item:saved":
                    //adaptiveCardData.Title = "The item **" + Item.Name + "** is saved.";
                    adaptiveCardData.Description = "Changes to the  **" + Item.Name + "** item are saved on the [jbexmclouddemo](" + tenantUrl + ") tenant. \r The following field values have changed:";
                    foreach (var fieldChange in Changes.FieldChanges)
                    {
                        var fieldName = await XMC.Item.GetFieldNameAsync(
                            tenantUrl,
                            apiKey,
                            fieldChange.FieldId,
                            Item.Id,
                            log);
                        adaptiveCardData.Fields.Add(new Field("The value of the field **" + fieldName + "** has changed from **" + fieldChange.OriginalValue.Replace("\\", "") + "** to **" + fieldChange.Value.Replace("\\", "") + "**"));
                    }
                    break;
                case "item:copied":
                    adaptiveCardData.Title = "The item " + Item.Name + " is copied";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:moved":
                    adaptiveCardData.Title = "The item " + Item.Name + " is moved to another parent.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:renamed":
                    adaptiveCardData.Title = "The item " + Item.Name + " is renamed.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:sortorderchanged":
                    adaptiveCardData.Title = "The sort order of the item " + Item.Name + " changed.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "item:templateChanged":
                    adaptiveCardData.Title = "The template of the item " + Item.Name + " has changed.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "publish:begin":
                    adaptiveCardData.Title = "Raised when the publishing of the item starts.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "publish:end":
                    adaptiveCardData.Title = "Raised when the publishing of the item ends.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "publish:fail":
                    adaptiveCardData.Title = "Raised when the publishing of the item fails.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                case "publish:statusUpdated":
                    adaptiveCardData.Title = "Raised when the status of item publishing updates.";
                    adaptiveCardData.Description = adaptiveCardData.Title;
                    break;
                default: return null;
            }

            return adaptiveCardData;
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
