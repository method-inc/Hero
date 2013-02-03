using System;
using Hero.Interfaces;

namespace Hero
{
    public class RoleAbility : IEquatable<RoleAbility>
    {
        public IRole Role {get; private set;}
        public Ability Ability {get; private set;}

        public RoleAbility(IRole role, Ability ability)
        {
            Role = role;
            Ability = ability;
        }

        public bool Equals(RoleAbility other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Role.Equals(other.Role) && Ability.Equals(other.Ability);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RoleAbility) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Role.GetHashCode()*397) ^ Ability.GetHashCode();
            }
        }

        public static bool operator ==(RoleAbility left, RoleAbility right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RoleAbility left, RoleAbility right)
        {
            return !Equals(left, right);
        }

    }
}
