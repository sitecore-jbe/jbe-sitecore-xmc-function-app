using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class Changes
    {
        [JsonProperty("FieldChanges")]
        public List<FieldChange> FieldChanges { get; set; }

        [JsonProperty("PropertyChanges")]
        public List<object> PropertyChanges { get; set; }

        [JsonProperty("IsUnversionedFieldChanged")]
        public bool IsUnversionedFieldChanged { get; set; }

        [JsonProperty("IsSharedFieldChanged")]
        public bool IsSharedFieldChanged { get; set; }
    }
}
