namespace Common
{
    using Newtonsoft.Json;

    public partial class Response
    {
        public static Response FromJson(string json) => JsonConvert.DeserializeObject<Response>(json);
    }
}