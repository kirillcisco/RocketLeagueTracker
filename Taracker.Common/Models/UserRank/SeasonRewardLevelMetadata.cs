namespace Common
{
    using Newtonsoft.Json;
    using System;

    public partial class SeasonRewardLevelMetadata
    {
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("rankName")]
        public string RankName { get; set; }
    }
}