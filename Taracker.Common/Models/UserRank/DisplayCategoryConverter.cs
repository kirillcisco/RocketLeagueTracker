namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class DisplayCategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DisplayCategory) || t == typeof(DisplayCategory?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "General":
                    return DisplayCategory.General;

                case "Performance":
                    return DisplayCategory.Performance;

                case "Skill":
                    return DisplayCategory.Skill;
            }
            throw new Exception("Cannot unmarshal type DisplayCategory");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DisplayCategory)untypedValue;
            switch (value)
            {
                case DisplayCategory.General:
                    serializer.Serialize(writer, "General");
                    return;

                case DisplayCategory.Performance:
                    serializer.Serialize(writer, "Performance");
                    return;

                case DisplayCategory.Skill:
                    serializer.Serialize(writer, "Skill");
                    return;
            }
            throw new Exception("Cannot marshal type DisplayCategory");
        }

        public static readonly DisplayCategoryConverter Singleton = new DisplayCategoryConverter();
    }
}