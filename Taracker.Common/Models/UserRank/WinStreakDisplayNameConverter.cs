namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class WinStreakDisplayNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(WinStreakDisplayName) || t == typeof(WinStreakDisplayName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "WinStreak")
            {
                return WinStreakDisplayName.WinStreak;
            }
            throw new Exception("Cannot unmarshal type WinStreakDisplayName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WinStreakDisplayName)untypedValue;
            if (value == WinStreakDisplayName.WinStreak)
            {
                serializer.Serialize(writer, "WinStreak");
                return;
            }
            throw new Exception("Cannot marshal type WinStreakDisplayName");
        }

        public static readonly WinStreakDisplayNameConverter Singleton = new WinStreakDisplayNameConverter();
    }
}