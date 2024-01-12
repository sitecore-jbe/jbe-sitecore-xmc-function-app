using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.GetFieldInfo
{
    public class Result
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}