namespace Common
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public partial class UserInfo
    {
        [JsonProperty("userId")]
        public long? UserId { get; set; }

        [JsonProperty("isPremium")]
        public bool IsPremium { get; set; }

        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }

        [JsonProperty("isInfluencer")]
        public bool IsInfluencer { get; set; }

        [JsonProperty("countryCode")]
        public object CountryCode { get; set; }

        [JsonProperty("customAvatarUrl")]
        public object CustomAvatarUrl { get; set; }

        [JsonProperty("customHeroUrl")]
        public object CustomHeroUrl { get; set; }

        [JsonProperty("socialAccounts")]
        public List<object> SocialAccounts { get; set; }
    }
}