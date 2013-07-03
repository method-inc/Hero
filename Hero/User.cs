using System;
using System.Collections.Generic;
using Hero.Interfaces;

namespace Hero
{
    public class User : IUser, IEquatable<User>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<IAbility> Abilities { get; set; }

        public IList<IRole> Roles { get; set; }

        public User() { }

        public User(string name)
        {
            Name = name;
            Id = name;
            Abilities = new List<IAbility>();
            Roles = new List<IRole>();
        }

        public User(string name, string id)
        {
            Name = name;
            Id = id;
            Abilities = new List<IAbility>();
            Roles = new List<IRole>();
        }

        public bool Equals(User other)
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
