using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL
{
    public class UpdateItemResult
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}