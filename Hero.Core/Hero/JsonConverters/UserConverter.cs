using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;
using Newtonsoft.Json;

namespace Hero.JsonConverters
{
    public class UserConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<User>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IUser));
        }
    }

    public class UserListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<User> users = serializer.Deserialize<List<User>>(reader);

            List<IUser> interfaces = new List<IUser>();
            interfaces.AddRange(users);
            return interfaces;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IList<IUser>));
        }
    }
}
