using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldName
{
    public class Definition
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}