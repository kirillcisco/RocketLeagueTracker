namespace Common
{
    using Newtonsoft.Json;
    using System;

    internal class CategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Category) || t == typeof(Category?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "general":
                    return Category.General;

                case "performance":
                    return Category.Performance;

                case "skill":
                    return Category.Skill;
            }
            throw new Exception("Cannot unmarshal type Category");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Category)untypedValue;
            switch (value)
            {
                case Category.General:
                    serializer.Serialize(writer, "general");
                    return;

                case Category.Performance:
                    serializer.Serialize(writer, "performance");
                    return;

                case Category.Skill:
                    serializer.Serialize(writer, "skill");
                    return;
            }
            throw new Exception("Cannot marshal type Category");
        }

        public static readonly CategoryConverter Singleton = new CategoryConverter();
    }
}