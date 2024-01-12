using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.UpdateItem
{
    public class UpdateItem
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }
}
