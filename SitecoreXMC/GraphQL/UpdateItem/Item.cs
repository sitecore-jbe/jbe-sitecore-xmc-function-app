using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.UpdateItem
{
    public class Item
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
    }
}