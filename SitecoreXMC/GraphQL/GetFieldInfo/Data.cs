using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldInfo
{
    public class Data
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }
}