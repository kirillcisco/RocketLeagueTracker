namespace Common
{
    using Newtonsoft.Json;
    using System;

    public partial class TierMetadata
    {
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("estimated", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Estimated { get; set; }
    }
}