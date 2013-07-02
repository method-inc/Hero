using System;
using Hero.Interfaces;

namespace Hero
{
    public class Role : IRole, IEquatable<Role>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
            Id = name;
        }

        public Role(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public bool Equals(Role other)
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
            return Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            return Id.ToLower().GetHashCode();
        }

        public static bool operator ==(Role left, Role right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Role left, Role right)
        {
            return !Equals(left, right);
        }
    }
}
