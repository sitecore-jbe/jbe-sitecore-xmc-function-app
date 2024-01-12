using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldInfo
{
    public class Field
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("definition")]
        public Definition Definition { get; set; }
    }
}