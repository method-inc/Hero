using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;
using Newtonsoft.Json;

namespace Hero.JsonConverters
{
    public class RoleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Role>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IRole));
        }
    }

    public class RoleListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<Role> roles = serializer.Deserialize<List<Role>>(reader);

            List<IRole> interfaces = new List<IRole>();
            interfaces.AddRange(roles);
            return interfaces;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IList<IRole>));
        }
    }
}
