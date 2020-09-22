namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class AvailableSegmentTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AvailableSegmentType) || t == typeof(AvailableSegmentType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "overview":
                    return AvailableSegmentType.Overview;

                case "playlist":
                    return AvailableSegmentType.Playlist;
            }
            throw new Exception("Cannot unmarshal type AvailableSegmentType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AvailableSegmentType)untypedValue;
            switch (value)
            {
                case AvailableSegmentType.Overview:
                    serializer.Serialize(writer, "overview");
                    return;

                case AvailableSegmentType.Playlist:
                    serializer.Serialize(writer, "playlist");
                    return;
            }
            throw new Exception("Cannot marshal type AvailableSegmentType");
        }

        public static readonly AvailableSegmentTypeConverter Singleton = new AvailableSegmentTypeConverter();
    }
}