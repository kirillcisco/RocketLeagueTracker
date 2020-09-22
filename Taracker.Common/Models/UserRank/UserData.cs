namespace Common
{
    using Common.Search;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class UserData
    {
        [JsonProperty("platformInfo")]
        public PlatformInfo PlatformInfo { get; set; }

        [JsonProperty("userInfo")]
        public UserInfo UserInfo { get; set; }

        [JsonProperty("metadata")]
        public DataMetadata Metadata { get; set; }

        [JsonProperty("segments")]
        public List<Segment> Segments { get; set; }

        [JsonProperty("availableSegments")]
        public List<AvailableSegment> AvailableSegments { get; set; }

        [JsonProperty("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

    }
}