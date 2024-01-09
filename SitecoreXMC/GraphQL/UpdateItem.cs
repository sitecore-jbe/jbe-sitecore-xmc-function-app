using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL
{
    public class UpdateItem
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }
}
