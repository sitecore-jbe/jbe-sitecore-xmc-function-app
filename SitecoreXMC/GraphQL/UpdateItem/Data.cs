using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.UpdateItem
{
    public class Data
    {
        [JsonProperty("updateItem")]
        public UpdateItem UpdateItem { get; set; }
    }
}