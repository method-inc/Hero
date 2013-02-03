using System;
using System.Collections.Generic;
using Hero.Interfaces;

namespace Hero
{
    public class Role : IRole, IEquatable<Role>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            return Id;
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
