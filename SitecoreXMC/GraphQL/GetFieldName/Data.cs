using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldName
{
    public class Data
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }
}