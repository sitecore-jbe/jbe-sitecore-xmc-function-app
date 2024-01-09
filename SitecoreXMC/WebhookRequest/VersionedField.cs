using System;
using Newtonsoft.Json;

namespace Sitecore.XMC.WebhookRequest
{
    public class VersionedField
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Version", NullValueHandling = NullValueHandling.Ignore)]
        public long? Version { get; set; }
    }
}