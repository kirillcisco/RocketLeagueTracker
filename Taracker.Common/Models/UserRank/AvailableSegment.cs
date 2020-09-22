namespace Common
{
    using Newtonsoft.Json;

    public partial class AvailableSegment
    {
        [JsonProperty("type")]
        public AvailableSegmentType Type { get; set; }

        [JsonProperty("attributes")]
        public AvailableSegmentAttributes Attributes { get; set; }

        [JsonProperty("metadata")]
        public AvailableSegmentMetadata Metadata { get; set; }
    }
}