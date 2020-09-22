namespace Common
{
    using Newtonsoft.Json;

    public partial class AvailableSegmentMetadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}