using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL
{
    public class Item
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
    }
}