using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;
using Newtonsoft.Json;

namespace Hero.JsonConverters
{
    public class AbilityConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Ability>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IAbility));
        }
    }

    public class AbilityListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<Ability> abilities = serializer.Deserialize<List<Ability>>(reader);

            List<IAbility> interfaces = new List<IAbility>();
            interfaces.AddRange(abilities);
            return interfaces;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IList<IAbility>));
        }
    }
}
