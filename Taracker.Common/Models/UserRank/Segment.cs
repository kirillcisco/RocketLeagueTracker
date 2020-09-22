namespace Common
{
    using Newtonsoft.Json;
    using System;

    public partial class Segment
    {
        [JsonProperty("type")]
        public AvailableSegmentType Type { get; set; }

        [JsonProperty("attributes")]
        public SegmentAttributes Attributes { get; set; }

        [JsonProperty("metadata")]
        public AvailableSegmentMetadata Metadata { get; set; }

        [JsonProperty("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
    }
}