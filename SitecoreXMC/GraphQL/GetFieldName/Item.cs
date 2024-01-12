using System.Collections.Generic;
using Newtonsoft.Json;
namespace Sitecore.XMC.GraphQL.GetFieldName
{
    public class Item
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }
}