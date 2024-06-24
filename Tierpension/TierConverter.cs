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

            // Überprüfe, ob das JSON-Dokument die erforderlichen Felder enthält
            if (jsonObject["Tierart"] != null && jsonObject["Tiername"] != null &&
                jsonObject["Fixpreis"] != null && jsonObject["Tagespreis"] != null &&
                jsonObject["Essen"] != null)
            {
                string tierArt = jsonObject["Tierart"].ToString();
                string tierName = jsonObject["Tiername"].ToString();

                if (string.IsNullOrEmpty(tierName))
                {
                    throw new Exception("Der Tiername im JSON-Dokument ist ungültig oder fehlt.");
                }

                Tier tier = tierArt switch
                {
                    "Hund" => new Hund(
                        tierArt,
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Katze" => new Katze(
                        tierArt,
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Wellensittich" => new Wellensittich(
                        tierArt,
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Kaninchen" => new Kaninchen(
                        tierArt,
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    "Meerschweinchen" => new Meerschweinchen(
                        tierArt,
                        tierName,
                        jsonObject["Fixpreis"].Value<decimal>(),
                        jsonObject["Tagespreis"].Value<decimal>(),
                        jsonObject["Essen"].ToObject<List<string>>()),
                    _ => throw new Exception($"Unbekannte Tierart: {tierArt}")
                };

                serializer.Populate(jsonObject.CreateReader(), tier);
                return tier;
            }
            else
            {
                throw new Exception("Das JSON-Dokument enthält nicht alle erforderlichen Felder oder ist leer.");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
