namespace Common
{
    using Newtonsoft.Json;

    public partial class DivisionMetadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("estimated", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Estimated { get; set; }

        [JsonProperty("deltaDown", NullValueHandling = NullValueHandling.Ignore)]
        public long? DeltaDown { get; set; }

        [JsonProperty("deltaUp", NullValueHandling = NullValueHandling.Ignore)]
        public long? DeltaUp { get; set; }
    }
}