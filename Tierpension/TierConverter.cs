using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Tierpension
{
    public class TierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Tier).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            // Überprüfe, ob das JSON-Dokument ein 'Tier'-Feld enthält
            if (jsonObject["Name"] != null && jsonObject["Fixpreis"] != null && jsonObject["Tagespreis"] != null && jsonObject["Essen"] != null)
            {
                string tierName = jsonObject["Name"]?.ToString();
                if (string.IsNullOrEmpty(tierName))
                {
                    throw new Exception("Der Tiername im JSON-Dokument ist ungültig oder fehlt.");
                }

                Tier tier = tierName switch
                {
                    "Hund" => new Hund(
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Katze" => new Katze(
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Wellensittich" => new Wellensittich(
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    _ => throw new Exception($"Unbekannter TierTyp: {tierName}")
                };

                serializer.Populate(jsonObject.CreateReader(), tier);
                return tier;
            }
            else
            {
                throw new Exception("Das JSON-Dokument enthält kein 'Tier'-Feld oder es ist leer.");
            }
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("WriteJson is not implemented");
        }
    }
}
