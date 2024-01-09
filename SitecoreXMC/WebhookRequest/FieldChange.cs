using System;
using Newtonsoft.Json;

namespace Sitecore.XMC.WebhookRequest
{
    public class FieldChange
    {
        [JsonProperty("FieldId")]
        public Guid FieldId { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("OriginalValue")]
        public string OriginalValue { get; set; }
    }
}
