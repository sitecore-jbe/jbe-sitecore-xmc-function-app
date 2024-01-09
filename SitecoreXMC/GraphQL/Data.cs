using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL
{
    public class Data
    {
        [JsonProperty("updateItem")]
        public UpdateItem UpdateItem { get; set; }
    }
}