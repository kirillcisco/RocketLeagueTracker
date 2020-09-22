namespace Common
{
    using Newtonsoft.Json;

    public partial class SegmentAttributes
    {
        [JsonProperty("playlistId", NullValueHandling = NullValueHandling.Ignore)]
        public long? PlaylistId { get; set; }

        [JsonProperty("season", NullValueHandling = NullValueHandling.Ignore)]
        public long? Season { get; set; }
    }
}