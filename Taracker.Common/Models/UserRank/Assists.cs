
namespace Common
{
    using Newtonsoft.Json;

    public partial class Assists
    {
        [JsonProperty("rank")]
        public long? Rank { get; set; }

        [JsonProperty("percentile")]
        public double? Percentile { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayCategory")]
        public DisplayCategory DisplayCategory { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("metadata")]
        public AssistsMetadata Metadata { get; set; }

        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

        [JsonProperty("displayType")]
        public DisplayType DisplayType { get; set; }
    }
}
