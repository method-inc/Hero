using System;
using System.Collections.Generic;
using Hero.Interfaces;
using Hero.JsonConverters;
using Newtonsoft.Json;

namespace Hero
{
    public class Ability : IAbility, IEquatable<Ability>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        [JsonConverter(typeof(AbilityConverter))]
        public virtual IAbility Parent { get; set; }

        [JsonConverter(typeof(AbilityListConverter))]
        public virtual IList<IAbility> Abilities { get; set; }

        public Ability() { }

        public Ability(string name)
        {
            Name = name;
            Id = name;
            Abilities = new List<IAbility>();
        }

        public Ability(string name, string id)
        {
            Name = name;
            Id = id;
            Abilities = new List<IAbility>();
		}

        public virtual bool Equals(Ability other)
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
