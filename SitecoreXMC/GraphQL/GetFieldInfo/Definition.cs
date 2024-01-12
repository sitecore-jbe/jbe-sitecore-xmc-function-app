using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldInfo
{
    public class Definition
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}