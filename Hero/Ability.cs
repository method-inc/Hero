using System;
using DotNetStandard.Interfaces;
using Hero.Interfaces;

namespace Hero
{
    public class Ability : IAbility, IEquatable<Ability>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Ability() { }

        public Ability(string name)
        {
            Name = name;
            Id = name;
        }

        public Ability(string name, string id)
        {
            Name = name;
            Id = id;
		}

        public bool Equals(Ability other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.ToLower() ==  other.Id.ToLower();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ability) obj);
        }

        public override int GetHashCode()
        {
            return Id.ToLower().GetHashCode();
        }

        public static bool operator ==(Ability left, Ability right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Ability left, Ability right)
        {
            return !Equals(left, right);
        }
    }
}
