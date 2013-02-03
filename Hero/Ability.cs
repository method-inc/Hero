using System;
using DotNetStandard.Interfaces;

namespace Hero
{
    public class Ability : INameable, IEquatable<Ability>
    {
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        public Ability(string name)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            _name = name;
        }

        public bool Equals(Ability other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_name, other._name);
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
            return _name.GetHashCode();
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
