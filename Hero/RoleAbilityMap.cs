using System.Collections.Generic;
using Hero.Interfaces;

namespace Hero
{
    public class RoleAbilityMap : Dictionary<IRole, HashSet<Ability>> { }
}