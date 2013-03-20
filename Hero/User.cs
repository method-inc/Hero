using System.Collections.Generic;
using System.Linq;
using Hero.Interfaces;

namespace Hero
{
    public class User : IUser
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<IRole> Roles { get; private set; }

        public User(int id, string name, IEnumerable<IRole> roles)
        {
            Id = id;
            Name = name;
            Roles = roles;
        }

        public bool Is(IRole role)
        {
            return Roles.Contains(role);
        }

        public bool Is(string roleName)
        {
            return Roles.Any(r => r.Name == roleName);
        }

        public bool Equals(User other)
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
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return Id;
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
