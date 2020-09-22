namespace Common
{
    using Newtonsoft.Json;

    public static class Serialize
    {
        public static string ToJson(this Response self) => JsonConvert.SerializeObject(self);
    }
}