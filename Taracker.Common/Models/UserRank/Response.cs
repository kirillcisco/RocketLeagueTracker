namespace Common
{
    using Newtonsoft.Json;

    public partial class Response
    {
        [JsonProperty("data")]
        public UserData Data { get; set; }
    }
}