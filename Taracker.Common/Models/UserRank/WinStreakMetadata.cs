namespace Common
{
    using Newtonsoft.Json;

    public partial class WinStreakMetadata
    {
        [JsonProperty("type")]
        public MetadataType Type { get; set; }
    }
}