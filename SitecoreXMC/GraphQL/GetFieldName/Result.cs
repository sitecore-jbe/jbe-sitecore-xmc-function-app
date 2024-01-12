using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldName
{
    public class Result
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}