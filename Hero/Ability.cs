using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetStandard.Interfaces;

namespace Hero
{
    public class Ability : INameable, IEquatable<Ability>
    {
        private readonly string _name;
        private IEnumerable<Ability> _children;

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<Ability> Children
        {
            get { return _children; }
            set
            {
                if (value.Any(ability => ability == this))
                {
                    throw new InvalidOperationException("You cannot set one of the children of an Ability to itself.");
                }

                _children = value;
            }
        }

        public Ability(string name)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            _name = name;
            _children = new List<Ability>();
        }

        public Ability(string name, IEnumerable<Ability> children )
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (children == null)
                throw new ArgumentNullException("parent");

            _children = children;
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
