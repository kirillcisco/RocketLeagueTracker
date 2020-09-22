namespace Common
{
    using Newtonsoft.Json;

    public partial class DataMetadata
    {
        [JsonProperty("lastUpdated")]
        public LastUpdated LastUpdated { get; set; }

        [JsonProperty("playerId")]
        public long? PlayerId { get; set; }
    }
}