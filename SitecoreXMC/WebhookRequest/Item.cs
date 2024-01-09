using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sitecore.XMC
{
    public class Item
    {
        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Version")]
        public long Version { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ParentId")]
        public Guid ParentId { get; set; }

        [JsonProperty("TemplateId")]
        public Guid TemplateId { get; set; }

        [JsonProperty("TemplateName")]
        public string TemplateName { get; set; }

        [JsonProperty("MasterId")]
        public Guid MasterId { get; set; }

        [JsonProperty("SharedFields")]
        public List<SharedField> SharedFields { get; set; }

        [JsonProperty("UnversionedFields")]
        public List<VersionedField> UnversionedFields { get; set; }

        [JsonProperty("VersionedFields")]
        public List<VersionedField> VersionedFields { get; set; }
    }
}
