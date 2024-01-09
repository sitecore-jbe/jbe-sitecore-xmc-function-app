using System;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class SharedField
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
