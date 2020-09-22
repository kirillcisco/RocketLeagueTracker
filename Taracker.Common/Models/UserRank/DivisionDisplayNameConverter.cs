namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class DivisionDisplayNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DivisionDisplayName) || t == typeof(DivisionDisplayName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Matches")
            {
                return DivisionDisplayName.Matches;
            }
            throw new Exception("Cannot unmarshal type DivisionDisplayName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DivisionDisplayName)untypedValue;
            if (value == DivisionDisplayName.Matches)
            {
                serializer.Serialize(writer, "Matches");
                return;
            }
            throw new Exception("Cannot marshal type DivisionDisplayName");
        }

        public static readonly DivisionDisplayNameConverter Singleton = new DivisionDisplayNameConverter();
    }
}