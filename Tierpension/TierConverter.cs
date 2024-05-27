using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Tierpension
{
    public class TierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Tier);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string tierName = jsonObject["Name"].ToObject<string>();

            switch (tierName)
            {
                case "Hund":
                    return jsonObject.ToObject<Hund>();
                case "Katze":
                    return jsonObject.ToObject<Katze>();
                case "Wellensittich":
                    return jsonObject.ToObject<Wellensittich>();
                default:
                    throw new NotSupportedException($"Unbekannter Tier-Typ: {tierName}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
