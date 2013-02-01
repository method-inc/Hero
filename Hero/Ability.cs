using System;

namespace Hero
{
    public class Ability
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
    }
}
