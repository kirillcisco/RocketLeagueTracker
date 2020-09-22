namespace Common
{
    using Newtonsoft.Json;

    public partial class Tier
    {
        [JsonProperty("rank")]
        public object Rank { get; set; }

        [JsonProperty("percentile")]
        public object Percentile { get; set; }

        [JsonProperty("displayName")]
        public DivisionDisplayName DisplayName { get; set; }

        [JsonProperty("displayCategory")]
        public DisplayCategory DisplayCategory { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("metadata")]
        public TierMetadata Metadata { get; set; }

        [JsonProperty("value")]
        public long? Value { get; set; }

        [JsonProperty("displayValue")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? DisplayValue { get; set; }

        [JsonProperty("displayType")]
        public DisplayType DisplayType { get; set; }
    }
}