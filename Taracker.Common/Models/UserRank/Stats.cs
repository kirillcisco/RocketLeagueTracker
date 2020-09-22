namespace Common
{
    using Newtonsoft.Json;

    public partial class Stats
    {
        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Wins { get; set; }

        [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Goals { get; set; }

        [JsonProperty("mVPs", NullValueHandling = NullValueHandling.Ignore)]
        public Assists MVPs { get; set; }

        [JsonProperty("saves", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Saves { get; set; }

        [JsonProperty("assists", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Assists { get; set; }

        [JsonProperty("shots", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Shots { get; set; }

        [JsonProperty("goalShotRatio", NullValueHandling = NullValueHandling.Ignore)]
        public Assists GoalShotRatio { get; set; }

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Score { get; set; }

        [JsonProperty("seasonRewardLevel", NullValueHandling = NullValueHandling.Ignore)]
        public SeasonRewardLevel SeasonRewardLevel { get; set; }

        [JsonProperty("seasonRewardWins", NullValueHandling = NullValueHandling.Ignore)]
        public Assists SeasonRewardWins { get; set; }

        [JsonProperty("tier", NullValueHandling = NullValueHandling.Ignore)]
        public Tier Tier { get; set; }

        [JsonProperty("division", NullValueHandling = NullValueHandling.Ignore)]
        public Division Division { get; set; }

        [JsonProperty("matchesPlayed", NullValueHandling = NullValueHandling.Ignore)]
        public Assists MatchesPlayed { get; set; }

        [JsonProperty("winStreak", NullValueHandling = NullValueHandling.Ignore)]
        public WinStreak WinStreak { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public Assists Rating { get; set; }
    }
}