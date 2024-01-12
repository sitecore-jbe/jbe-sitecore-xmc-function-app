using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldName
{
    public class Field
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("definition")]
        public Definition Definition { get; set; }
    }
}