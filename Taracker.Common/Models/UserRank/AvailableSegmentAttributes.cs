namespace Common
{
    using Newtonsoft.Json;

    public partial class AvailableSegmentAttributes
    {
        [JsonProperty("season")]
        public long? Season { get; set; }
    }
}