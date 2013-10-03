using System.Linq;
using System;
using System.Collections.Generic;
using Hero.Interfaces;
using Hero.JsonConverters;
using Newtonsoft.Json;

namespace Hero
{
    public class User : IUser, IEquatable<User>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        [JsonConverter(typeof(AbilityListConverter))]
        public virtual IList<IAbility> Abilities
        {
            get { return Roles.SelectMany(r => r.Abilities).ToList(); }
        }

        [JsonConverter(typeof(RoleListConverter))]
        public virtual IList<IRole> Roles { get; set; }

        public User() { }

        public User(string name)
        {
            Name = name;
            Id = name;
            Roles = new List<IRole>();
        }

        public User(string name, string id)
        {
            Name = name;
            Id = id;
            Roles = new List<IRole>();
        }

        public virtual bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.ToLower() == other.Id.ToLower();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return Id.ToLower().GetHashCode();
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}
