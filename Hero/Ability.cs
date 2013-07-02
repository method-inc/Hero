using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetStandard.Interfaces;
using Hero.Interfaces;

namespace Hero
{
    public class Ability : IAbility, IEquatable<Ability>
    {
        private IEnumerable<Ability> _children;

        public string Id { get; set; }
        public string Name { get; set; }

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

        public Ability()
        {
            _children = new List<Ability>();
        }

        public Ability(string name)
        {
            Name = name;
            Id = name;
            _children = new List<Ability>();
        }

        public Ability(string name, string id, IEnumerable<Ability> children)
        {
            Name = name;
            Id = id;
            if (children == null)
                throw new ArgumentNullException("parent");

            _children = children;
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
