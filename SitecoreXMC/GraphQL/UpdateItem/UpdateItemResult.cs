using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.UpdateItem
{
    public class Result
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}