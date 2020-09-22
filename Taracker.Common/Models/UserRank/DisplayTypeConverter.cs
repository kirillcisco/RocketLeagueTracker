namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class DisplayTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DisplayType) || t == typeof(DisplayType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Number":
                    return DisplayType.Number;

                case "NumberPrecision1":
                    return DisplayType.NumberPrecision1;
            }
            throw new Exception("Cannot unmarshal type DisplayType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DisplayType)untypedValue;
            switch (value)
            {
                case DisplayType.Number:
                    serializer.Serialize(writer, "Number");
                    return;

                case DisplayType.NumberPrecision1:
                    serializer.Serialize(writer, "NumberPrecision1");
                    return;
            }
            throw new Exception("Cannot marshal type DisplayType");
        }

        public static readonly DisplayTypeConverter Singleton = new DisplayTypeConverter();
    }
}