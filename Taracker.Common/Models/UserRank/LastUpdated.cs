namespace Common
{
    using Newtonsoft.Json;
    using System;

    public partial class LastUpdated
    {
        [JsonProperty("value")]
        public DateTimeOffset? Value { get; set; }

        [JsonProperty("displayValue")]
        public DateTimeOffset? DisplayValue { get; set; }
    }
}