using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sitecore.XMC.GraphQL.GetFieldInfo
{
    public class Item
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }
}