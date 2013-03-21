using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero
{
    public class UserRole: IEquatable<UserRole>
    {
        public IRole Role {get; private set;}
        public IUser User {get; private set;}

        public UserRole(IUser user, IRole role)
        {
            Role = role;
            User = user;
        }

        public bool Equals(UserRole other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Role.Equals(other.Role) && User.Equals(other.User);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserRole) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Role.GetHashCode()*397) ^ User.GetHashCode();
            }
        }

        public static bool operator ==(UserRole left, UserRole right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserRole left, UserRole right)
        {
            return !Equals(left, right);
        }

    }
}
